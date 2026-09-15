using Core.DataAccess.EntityFramework;
using Goods.Api.Goods.Api.DataAccess.Abstract;
using Goods.Api.Goods.Api.DataAccess.Concrete.Context;
using Goods.Api.Goods.Api.Entities.Concrete;

namespace Goods.Api.Goods.Api.DataAccess.Concrete
{
    public class EfCategoryDal : EfBaseRepository<Category, GoodsDbContext>, ICategoryDal
    {
    }
}
