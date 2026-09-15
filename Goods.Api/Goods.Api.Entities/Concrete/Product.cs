using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Goods.Api.Goods.Api.Entities.Concrete
{
    [Table("Products")]
    public class Product : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        public string Name { get; set; }

        public Int64 CategoryId { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}