namespace Goods.Api.Goods.Api.Entities.Dtos
{
    public class CategoryUpdateDto
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}