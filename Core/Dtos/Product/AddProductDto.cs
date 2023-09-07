using Core.Dtos.Brand;
using Core.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Product
{
    public class AddUpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }


    }
}
