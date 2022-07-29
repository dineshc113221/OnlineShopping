using Microsoft.AspNetCore.Http;
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
    public class ProductController : ControllerBase
    {
        online_shoppingContext db = new online_shoppingContext();
        [HttpGet]
        [Route("GetProducts")]
        public IActionResult GetProducts()
        {

            var data = from prod in db.Products where prod.RetId != null select prod;
            
            return Ok(data);
        }

        [HttpGet]
        [Route("GetProducts/{id}")]
        public IActionResult GetProducts(int? id)
        {
            var data = from prod in db.Products where prod.ProductId==id && prod.RetId!=null select prod;
            if(data==null)
            {
                return BadRequest($"Product {id} does not exist");
            }
            return Ok(data);
        }
        
        [HttpGet]
        [Route("GetCategory/{id}")]
        public IActionResult GetCategory(int? id)
        {
            var data = from prod in db.Products.Include("Category") where prod.CategoryId == id select new { Category=prod.Category.CategoryName, ProductName = prod.ProductName,ProductBrand=prod.Brand, ProductDescription = prod.ProdDesc, Price = prod.Price };
            return Ok(data);
        }

        [HttpGet]
        [Route("GetBrand/{bname}")]
        public IActionResult GetBrand(string? bname)
        {
            var data = from prod in db.Products where prod.Brand==bname select new { ProductBrand = prod.Brand,ProductName = prod.ProductName, ProductDescription = prod.ProdDesc, Price = prod.Price };
            return Ok(data);
        }


    }
}
