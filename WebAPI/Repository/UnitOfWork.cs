using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.IRepository;

namespace WebAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly APIDBContext _context;

        private IGenericRepository<Product> _product;
        private IGenericRepository<Customer> _customer;
        private IGenericRepository<Invoice> _invoice; 

        public UnitOfWork(APIDBContext context)
        {
            _context = context;
        }

        public IGenericRepository<Product> Product => _product ??= new GenericRepository<Product>(_context);

        public IGenericRepository<Customer> Customer => _customer ??= new GenericRepository<Customer>(_context);
        public IGenericRepository<Invoice> Invoice => _invoice ??= new GenericRepository<Invoice>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
