using Core.Utilities.Responses;
using Goods.Api.Goods.Api.Business.Abstract;
using Goods.Api.Goods.Api.Business.Concrete;
using Goods.Api.Goods.Api.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Goods.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiDataResponse<List<ProductDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetList()
        {
            var result = await _productService.GetListAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiDataResponse<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByNameAsync([FromBody] string productName)
        {
            var result = await _productService.GetByNameAsync(productName);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiDataResponse<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(Int64 Id)
        {
            var result = await _productService.GetByIdAsync(Id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiDataResponse<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromBody] ProductCreatedDto command)
        {
            var result = await _productService.AddAsync(command);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiDataResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteById(Int64 Id)
        {
            var result = await _productService.DeleteByIdAsync(Id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiDataResponse<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] ProductUpdateDto command)
        {
            var result = await _productService.UpdateAsync(command);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }



    }
}