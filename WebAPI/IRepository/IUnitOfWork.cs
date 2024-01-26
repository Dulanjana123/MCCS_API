using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;

namespace WebAPI.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Product { get; }
        IGenericRepository<Customer> Customer { get; }
        IGenericRepository<Invoice> Invoice { get; }
        Task Save();
    }
}
