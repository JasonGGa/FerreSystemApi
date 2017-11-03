using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FerreSystemApi.Models;

namespace FerreSystemApi.Controllers
{
    [Route("api/ferre")]
    public class FerreController : Controller
    {
        private readonly FerreContext _context;

        public FerreController(FerreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(int id)
        {
            var item = _context.Products.FirstOrDefault(t => t.ProductId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Products.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.ProductId }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product itemup)
        {
            if (itemup == null || itemup.ProductId != id)
            {
                return BadRequest();
            }

            var item = _context.Products.FirstOrDefault(t => t.ProductId == id);
            if (item == null)
            {
                return NotFound();
            }
            
            item.Name = itemup.Name;
            item.PurchPriceSol = itemup.PurchPriceSol;
            item.PurchPriceDol = itemup.PurchPriceDol;
            item.WholesalePrice = itemup.WholesalePrice;
            item.RetailPrice = itemup.RetailPrice;
            item.Stock = itemup.Stock;

            _context.Products.Update(item);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpPut("{ex}")]
        public IActionResult UpdateExchange(double exRate)
        {
            IQueryable<Product> plist = _context.Products.Where(t => t.PurchPriceDol != null);
            if (plist == null)
            {
                return NotFound();
            }

            foreach (var item in plist)
            {
                item.PurchPriceSol = item.PurchPriceDol * exRate;
                _context.Products.Update(item);
            }

            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.Products.First(t => t.ProductId == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Products.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }        
    }
}