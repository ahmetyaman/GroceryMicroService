using Core.Utilities.Responses;
using Goods.Api.Goods.Api.Entities.Dtos;

namespace Goods.Api.Goods.Api.Business.Abstract
{
    public interface ICategoryService
    {
        Task<ApiDataResponse<CategoryDto>> AddAsync(CategoryCreatedDto command);
        Task<ApiDataResponse<bool>> DeleteByIdAsync(long id);
        Task<ApiDataResponse<CategoryDto>> GetByIdAsync(long id);
        Task<ApiDataResponse<CategoryDto>> GetByNameAsync(string categoryName);
        Task<ApiDataResponse<List<CategoryDto>>> GetListAsync();
        Task<ApiDataResponse<CategoryDto>> UpdateAsync(CategoryUpdateDto command);
    }
}