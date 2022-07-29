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
    public class RetailerController : ControllerBase
    {
        RetailerRepository repos = new RetailerRepository();

        [HttpPost]
        [Route("AddRetailer")]
        public IActionResult AddRetailer(Retailer ret)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repos.AddRetailer(ret);
                    return Created("Retailer added successfully", ret);
                }
                catch
                {
                    return BadRequest("Something went wrong while saving the record");

                }
            }
            return BadRequest("Something went wrong while saving the record");
        }

        [HttpGet]
        [Route("GetRetailers")]

        public IActionResult GetRetailers()
        {
            List<Retailer> retailers= repos.GetRetailers();

            if(retailers.Count()==0)
            {
                return NotFound("No retailers present");
            }
            return Ok(retailers);
        }
        
    }
}
