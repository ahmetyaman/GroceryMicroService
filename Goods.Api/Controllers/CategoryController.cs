using Core.Utilities.Responses;
using Goods.Api.Goods.Api.Business.Abstract;
using Goods.Api.Goods.Api.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Goods.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiDataResponse<List<CategoryDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetList()
        {
            var result = await _categoryService.GetListAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiDataResponse<CategoryDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByNameAsync([FromBody] string categoryName)
        {
            var result = await _categoryService.GetByNameAsync(categoryName);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiDataResponse<CategoryDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(Int64 Id)
        {
            var result = await _categoryService.GetByIdAsync(Id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiDataResponse<CategoryDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromBody] CategoryCreatedDto command)
        {
            var result = await _categoryService.AddAsync(command);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiDataResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteById(Int64 Id)
        {
            var result = await _categoryService.DeleteByIdAsync(Id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiDataResponse<CategoryDto>), (int)HttpStatusCode.OK)]    
        public async Task<IActionResult> Update([FromBody] CategoryUpdateDto command)
        {
            var result = await _categoryService.UpdateAsync(command);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

    }
}