using AutoMapper;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Messages;
using Core.Utilities.Responses;
using Goods.Api.Goods.Api.Business.Abstract;
using Goods.Api.Goods.Api.DataAccess.Abstract;
using Goods.Api.Goods.Api.Entities.Concrete;
using Goods.Api.Goods.Api.Entities.Dtos;

namespace Goods.Api.Goods.Api.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductDal _ProductDal;

        private readonly IMapper _mapper;

        public ProductService(IProductDal ProductDal, IMapper mapper)
        {
            _ProductDal = ProductDal;
            _mapper = mapper;
        }

        public async Task<ApiDataResponse<ProductDto>> AddAsync(ProductCreatedDto command)
        {
            Product Product = _mapper.Map<Product>(command);

            var response = await _ProductDal.AddAsync(Product);
            var responsedata = _mapper.Map<ProductDto>(response);

            return new SuccessApiDataResponse<ProductDto>(responsedata, message: ResultCodes.HTTP_OK.ToString());
        }

        public async Task<ApiDataResponse<bool>> DeleteByIdAsync(long Id)
        {
            bool response = await _ProductDal.DeleteAsync((int)Id);

            return new SuccessApiDataResponse<bool>(response, message: ResultCodes.HTTP_OK.ToString());
        }

        public async Task<ApiDataResponse<ProductDto>> GetByIdAsync(long id)
        {
            var response = await _ProductDal.GetAsync(c => c.Id == id);
            var responsedata = _mapper.Map<ProductDto>(response);

            return new SuccessApiDataResponse<ProductDto>(responsedata, message: ResultCodes.HTTP_OK.ToString());
        }

        public async Task<ApiDataResponse<ProductDto>> GetByNameAsync(string productName)
        {
            var response = await _ProductDal.GetAsync(c => string.Equals(c.Name, productName));
            var responsedata = _mapper.Map<ProductDto>(response);

            return new SuccessApiDataResponse<ProductDto>(responsedata, message: ResultCodes.HTTP_OK.ToString());
        }

        [PerformanceCounterAspect]
        public async Task<ApiDataResponse<List<ProductDto>>> GetListAsync()
        {
            var response = await _ProductDal.GetListAsync();
            var responsedata = _mapper.Map<List<ProductDto>>(response);

            return new SuccessApiDataResponse<List<ProductDto>>(responsedata, message: ResultCodes.HTTP_OK.ToString(), resultCount: responsedata.Count);
        }

        public async Task<ApiDataResponse<ProductDto>> UpdateAsync(ProductUpdateDto command)
        {
            Product Product = _mapper.Map<Product>(command);

            var response = await _ProductDal.UpdateAsync(Product);
            var responsedata = _mapper.Map<ProductDto>(response);

            return new SuccessApiDataResponse<ProductDto>(responsedata, message: ResultCodes.HTTP_OK.ToString());
        }
    }
}