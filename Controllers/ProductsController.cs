using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FashionEcommerceAPI.Data;
using FashionEcommerceAPI.Models;

namespace FashionEcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Products>> GetProducts()
        {
            try
            {
                var products = _context.Products.ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public ActionResult<Products> GetProduct(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                    return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<Products> CreateProduct(Products product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, Products product)
        {
            if (id != product.Id)
                return BadRequest("Product ID mismatch");

            try
            {
                var existingProduct = _context.Products.Find(id);
                if (existingProduct == null)
                    return NotFound();

                // Update fields
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.ImageUrl = product.ImageUrl;
                existingProduct.Category = product.Category;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.UpdatedAt = DateTime.UtcNow;

                _context.Entry(existingProduct).State = EntityState.Modified;
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                    return NotFound();

                _context.Products.Remove(product);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
