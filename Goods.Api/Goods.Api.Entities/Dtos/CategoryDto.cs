using Core.Entities.Dto;

namespace Goods.Api.Goods.Api.Entities.Dtos
{
    public class CategoryDto : IDto
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}