using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWRetail.Context;
using CWRetail.DTO;
using CWRetail.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CWRetail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _context.Products.ToArrayAsync());
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] ProductDto productDto)
        {
            var p = new Product()
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Type = productDto.Type,
                Active = productDto.Active,
            };

            _context.Products.Add(p);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetProduct",
                new { id = p.Id },
                p
            );
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            var prod = await _context.Products.FindAsync(id);

            if (prod == null)
            {
                return NotFound();
            }

            _context.ChangeTracker.Clear();
            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
