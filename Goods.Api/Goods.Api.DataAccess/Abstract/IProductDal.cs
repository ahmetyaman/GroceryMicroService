using Core.DataAccess;
using Goods.Api.Goods.Api.Entities.Concrete;

namespace Goods.Api.Goods.Api.DataAccess.Abstract
{
    public interface IProductDal : IBaseRepository<Product>
    {
    }
}
