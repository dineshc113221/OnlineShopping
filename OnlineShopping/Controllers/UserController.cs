using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.models;
using OnlineShopping.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        /// <summary>
        
        /// </summary>
        UserRepository repos = new UserRepository();
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            List<User> users=repos.GetUserList();
            if(users.Count()==0)
            {
                return NotFound("No users present");

            }
            return Ok(users);
        }


        [HttpGet]
        [Route("GetUsers/{id}")]
        public IActionResult GetUsersId(int id)
        {
            User user = repos.GetUserId(id);
            if (user==null)
            {
                return NotFound($"No user with id {id} present");

            }
            return Ok(user);
        }


        [HttpPost]
        [Route("GetUsers")]
        public IActionResult GetUserDetail(User _user)
        {
            User user = repos.GetUserId(_user.UserId);
           
            return Ok(user);
        }


        [HttpPost]
        [Route("AddUser")]
       
        public IActionResult PostUser(User user)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    repos.AddUser(user);
                    return Created("Record added successfully",user);
                }
                catch
                {
                    return BadRequest("Something went wrong while saving the record");
                }
               
            }
            return BadRequest("Something went wrong while saving the record"); 
        }



    }
}
