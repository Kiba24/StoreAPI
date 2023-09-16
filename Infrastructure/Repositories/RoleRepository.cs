using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role> , IRoleRepository
    {
        public RoleRepository(StoreContext context) : base(context){}


    }
}
