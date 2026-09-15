using Core.Entities.Dto;

namespace Goods.Api.Goods.Api.Entities.Dtos
{
    public class ProductCreatedDto : IDto
    {
        public string Name { get; set; }
        public Int64 CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate
        {
            get; set;
        }
    }
}