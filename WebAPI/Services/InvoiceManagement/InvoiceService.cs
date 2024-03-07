using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.IRepository;
using WebAPI.Models;

namespace WebAPI.Services.InvoiceManagement
{
    public class InvoiceService : IInvoiceService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<InvoiceService> _logger;
        private readonly IMapper _mapper;

        public InvoiceService(IUnitOfWork unitOfWork, ILogger<InvoiceService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IList<InvoiceDTO>> GetInvoices()
        {
            var invoices = await _unitOfWork.Invoice.GetAll(includes: new List<string> { "Customer", "Products" });

            var results = _mapper.Map<IList<InvoiceDTO>>(invoices);

            return results;
        }
    }
}
