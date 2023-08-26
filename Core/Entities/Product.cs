using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("Brands")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        [ForeignKey("Categories")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }


    }
}
