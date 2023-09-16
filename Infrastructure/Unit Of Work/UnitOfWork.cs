using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.Unit_Of_Work
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Declarations
        private StoreContext _context;
        private IBrandRepository _brandRepository;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        #endregion

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        #region Implementations

        public IBrandRepository BrandRepository
        {
            get
            {
                if (_brandRepository == null) _brandRepository = new BrandRepository(_context);
                return _brandRepository;
            }
        }

        #endregion


        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null) _productRepository = new ProductRepository(_context);
                return _productRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null) _categoryRepository = new CategoryRepository(_context);
                return _categoryRepository;
            }
        }


        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null) _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }


        public IRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null) _roleRepository= new RoleRepository(_context);
                return _roleRepository;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
