using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Messages;
using Core.Utilities.Responses;
using Goods.Api.Goods.Api.Business.Abstract;
using Goods.Api.Goods.Api.DataAccess.Abstract;
using Goods.Api.Goods.Api.Entities.Concrete;
using Goods.Api.Goods.Api.Entities.Dtos;

namespace Goods.Api.Goods.Api.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        private readonly IMapper _mapper;

        public CategoryService(ICategoryDal categoryDal, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
        }

        public async Task<ApiDataResponse<CategoryDto>> AddAsync(CategoryCreatedDto command)
        {
            Category category = _mapper.Map<Category>(command);

            var response = await _categoryDal.AddAsync(category);
            var responsedata = _mapper.Map<CategoryDto>(response);

            return new SuccessApiDataResponse<CategoryDto>(responsedata, message: ResultCodes.HTTP_OK.ToString());
        }

        public async Task<ApiDataResponse<bool>> DeleteByIdAsync(long Id)
        {
            bool response = await _categoryDal.DeleteAsync((int)Id);

            return new SuccessApiDataResponse<bool>(response, message: ResultCodes.HTTP_OK.ToString());
        }

        public async Task<ApiDataResponse<CategoryDto>> GetByIdAsync(long id)
        {
            var response = await _categoryDal.GetAsync(c => c.Id == id);
            var responsedata = _mapper.Map<CategoryDto>(response);

            return new SuccessApiDataResponse<CategoryDto>(responsedata, message: ResultCodes.HTTP_OK.ToString());
        }

        public async Task<ApiDataResponse<CategoryDto>> GetByNameAsync(string categoryName)
        {
            var response = await _categoryDal.GetAsync(c => string.Equals(c.Name, categoryName));
            var responsedata = _mapper.Map<CategoryDto>(response);

            return new SuccessApiDataResponse<CategoryDto>(responsedata, message: ResultCodes.HTTP_OK.ToString());
        }

        [PerformanceCounterAspect]
        public async Task<ApiDataResponse<List<CategoryDto>>> GetListAsync()
        {
            var response = await _categoryDal.GetListAsync();
            var responsedata = _mapper.Map<List<CategoryDto>>(response);

            return new SuccessApiDataResponse<List<CategoryDto>>(responsedata, message: ResultCodes.HTTP_OK.ToString(), resultCount: responsedata.Count);
        }

        public async Task<ApiDataResponse<CategoryDto>> UpdateAsync(CategoryUpdateDto command)
        {
            Category category = _mapper.Map<Category>(command);

            var response = await _categoryDal.UpdateAsync(category);
            var responsedata = _mapper.Map<CategoryDto>(response);

            return new SuccessApiDataResponse<CategoryDto>(responsedata, message: ResultCodes.HTTP_OK.ToString());
        }
    }
}