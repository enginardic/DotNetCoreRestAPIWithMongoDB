using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IConnectionMultiplexer redis;

        private readonly ProductsService productsService;

        public ProductsController(ProductsService productsService, IConnectionMultiplexer redis)
        {
            this.productsService = productsService;
            this.redis = redis;
        }


        [HttpGet("{id:length(24)}")]
        public ActionResult<ProductModel> Get(string id)
        {
            var product = productsService.Get(id, out Result result);

            if (result == DefaultResults.InvalidRequest)
                return BadRequest();

            if (result == DefaultResults.NotFoundResult)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post(ProductModel product)
        {
            productsService.Add(product, out Result result);
            if (result == DefaultResults.InvalidRequest)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] ProductModel updatedProduct)
        {
            productsService.Update(id, updatedProduct, out Result result);

            if (result == DefaultResults.InvalidRequest)
                return BadRequest();

            if (result == DefaultResults.NotFoundResult)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            productsService.Delete(id, out Result result);

            if (result == DefaultResults.InvalidRequest)
                return BadRequest();

            if (result == DefaultResults.NotFoundResult)
                return NotFound();

            return NoContent();
        }
    }
}
