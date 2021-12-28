using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IConnectionMultiplexer redis;

        private readonly CategoriesService categoriesService;

        public CategoriesController(CategoriesService categoriesService, IConnectionMultiplexer redis)
        {
            this.categoriesService = categoriesService;
            this.redis = redis;
        }


        [HttpGet("{id:length(24)}")]
        public ActionResult<CategoryModel> Get(string id)
        {
            var category = categoriesService.Get(id, out Result result);

            if (result == DefaultResults.InvalidRequest)
                return BadRequest();

            if (result == DefaultResults.NotFoundResult)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public IActionResult Post(CategoryModel category)
        {
            categoriesService.Add(category, out Result result);
            if (result == DefaultResults.InvalidRequest)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] CategoryModel updatedCategory)
        {
            categoriesService.Update(id, updatedCategory, out Result result);

            if (result == DefaultResults.InvalidRequest)
                return BadRequest();

            if (result == DefaultResults.NotFoundResult)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            categoriesService.Delete(id, out Result result);

            if (result == DefaultResults.InvalidRequest)
                return BadRequest();

            if (result == DefaultResults.NotFoundResult)
                return NotFound();

            return NoContent();
        }
    }
}
