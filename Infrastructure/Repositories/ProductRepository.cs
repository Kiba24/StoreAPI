using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext context) : base(context)
        {
        }

        public override async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Brand)
                                           .Include(p => p.Category)
                                           .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(p => p.Brand)
                                          .Include(p => p.Category)
                                          .ToListAsync();
        }
    }
}
