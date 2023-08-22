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
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAT { get; set; }
    }
}
