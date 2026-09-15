using Core.Utilities.Responses;
using Goods.Api.Goods.Api.Entities.Dtos;

namespace Goods.Api.Goods.Api.Business.Abstract
{
    public interface IProductService
    {
        Task<ApiDataResponse<ProductDto>> AddAsync(ProductCreatedDto command);
        Task<ApiDataResponse<bool>> DeleteByIdAsync(long id);
        Task<ApiDataResponse<ProductDto>> GetByIdAsync(long id);
        Task<ApiDataResponse<ProductDto>> GetByNameAsync(string productName);
        Task<ApiDataResponse<List<ProductDto>>> GetListAsync();
        Task<ApiDataResponse<ProductDto>> UpdateAsync(ProductUpdateDto command);
    }
}
