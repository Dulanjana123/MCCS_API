using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebAPI.Services.InvoiceManagement;

namespace WebAPI.Services
{
    public class InvoiceHub : Hub<IInvoiceHub>
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceHub(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            await _invoiceService.GetInvoices();
        }
    }
}
