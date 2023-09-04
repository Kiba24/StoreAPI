using Core.Dtos.Brand;
using Core.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Product
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public BrandDto Brand { get; set; }
        public CategoryDto Category { get; set; }


    }
}
