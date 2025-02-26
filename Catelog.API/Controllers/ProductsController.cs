using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Catelog.Domain.Models;
using Catelog.Repository.Repositories;

namespace Catelog.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CatelogDbContext _context;

        public ProductsController(CatelogDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// get products list
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetProducts(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? productCode = null)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Page and PageSize must be greater than zero." });
            }

            var query = _context.Products.Include(x => x.Category).AsQueryable();

            if (!string.IsNullOrEmpty(productCode))
            {
                query = query.Where(p => p.Code.Contains(productCode));
            }

            int totalCount = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new
                {
                    id = p.Id,
                    name = p.Name,
                    code = p.Code,
                    categoryCode = p.Category.Code
                })
                .ToListAsync();

            return Ok(new
            {
                items = products,
                totalCount,
                totalPages,
                currentPage = page,
                pageSize
            });
        }

        
    }
}
