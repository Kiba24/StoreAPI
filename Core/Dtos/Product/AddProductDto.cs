using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Product
{
    public class AddProductDto
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}
