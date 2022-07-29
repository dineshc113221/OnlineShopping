﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopping.models;
using Microsoft.EntityFrameworkCore;

namespace OnlineShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
       
        online_shoppingContext db = new online_shoppingContext();
        [HttpGet]
        [Route("ListRetailers")]
        public IActionResult GetRetailers()
        {
            var data = from ret in db.Retailers select new {RetailersId=ret.RetId,RetailersName=ret.RetName,RetailersStatus=ret.IsAccepted};
            return Ok(data);
        }
        [HttpGet]
        [Route("ListRetailers/{id}")]
        public IActionResult GetRetailers(int? id)
        {
            var data = from ret in db.Retailers where ret.RetId==id select new { RetailersId = ret.RetId, RetailersName = ret.RetName, RetailersStatus = ret.IsAccepted };
            if (data == null)
            {
                return NotFound($"Retailer {id} is not present");
            }
            return Ok(data);
        }
        [HttpDelete]
        [Route("DeleteRetailer/{id}")]
        public IActionResult DeleteReatiler(int id)
        {
            var data = db.Retailers.Find(id);
            db.Retailers.Remove(data);
            db.SaveChanges();
            return Ok("Record Deleted");
        }
        [HttpPut]
        [Route("EditRetailer/{id}")]
        public IActionResult PutRetailer(int id,Retailer retailers)
        {
            if (ModelState.IsValid)
            {
                Retailer oret = db.Retailers.Find(id);
                oret.IsAccepted = true;
                db.SaveChanges();
                return Ok("Record Updated");
            }
            return BadRequest("unable to edit the record");
        }

        [HttpGet]
        [Route("ListProducts")]
        public IActionResult GetProucts()
        {
            var data = from prod in db.Products select prod;
            return Ok(data);
        }
        [HttpPost]
        [Route("AddRetailer")]
        public IActionResult PostRetailer(Retailer ret)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //db.Database.ExecuteSqlInterpolated($"AddRetailer {ret.RetName},{ret.RetEmail},{ret.RetMob},{ret.RetLoc},{ret.RetPwd}");
                    db.Retailers.Add(ret);
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.InnerException.Message);
                }
            }
            
            return Created("succesfully done",ret);

        }
    }
}
