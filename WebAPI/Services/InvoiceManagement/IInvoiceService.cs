using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.InvoiceManagement
{
    public interface IInvoiceService
    {
        public Task<IList<InvoiceDTO>> GetInvoices();
    }
}
