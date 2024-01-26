using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.IRepository;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<InvoiceController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public InvoiceController(IUnitOfWork unitOfWork, ILogger<InvoiceController> logger, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInvoices()
        {
            try
            {
                var invoices = await _unitOfWork.Invoice.GetAll(includes: new List<string> { "Customer", "Products" });

                var results = _mapper.Map<IList<InvoiceDTO>>(invoices);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetInvoice)}");
                return StatusCode(500, $"Internal Server Error. Please Try Again Later. {ex}");
            }
        }


        [HttpGet("GetInvoicesByCustomer/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInvoicesByCustomer(int customerId)
        {
            try
            {
                var invoices = await _unitOfWork.Invoice.GetAll(x => x.CustomerId == customerId, includes: new List<string> { "Customer", "Products" });

                var results = _mapper.Map<IList<InvoiceDTO>>(invoices);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetInvoice)}");
                return StatusCode(500, $"Internal Server Error. Please Try Again Later. {ex}");
            }
        }


        [HttpGet("{id:int}", Name = "GetInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInvoice(int id)
        {
            var invoice = await _unitOfWork.Invoice.Get(x => x.Id == id, includes: new List<string> { "Customer", "Products" });
            foreach(var item in invoice.Products)
            {
                item.Product= await _unitOfWork.Product.Get(x => x.Id == item.ProductId);
            }
            
            var result = _mapper.Map<InvoiceDTO>(invoice);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateInvoice([FromBody] CreateInvoiceDTO createInvoiceDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in the {nameof(GenerateInvoice)}");
                return BadRequest(ModelState);
            }

            try
            {
                // Map the DTO to the Invoice entity
                var invoice = _mapper.Map<Invoice>(createInvoiceDTO);

                // Calculate the total amount for all products
                decimal totalAmount = 0;
                foreach (var product in createInvoiceDTO.Products)
                {
                    totalAmount += (product.Amount - product.DiscountAmount) * product.ProductQty;
                }
                invoice.TotalAmount = totalAmount;
                
                invoice.CreatedDate = DateTime.Now;

                // Insert invoice into database
                await _unitOfWork.Invoice.Insert(invoice);
                await _unitOfWork.Save();

                return StatusCode(201, invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong in the {nameof(GenerateInvoice)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateInvoice(int id, [FromBody] InvoiceDTO invoiceDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid POST attempt in the {nameof(UpdateInvoice)}");
                return BadRequest(ModelState);
            }

            try
            {
                var invoice = await _unitOfWork.Invoice.Get(x => x.Id == id);
                if (invoice == null)
                {
                    _logger.LogError($"Invalid Update attempt in the {nameof(UpdateInvoice)}");
                    return BadRequest("Submited data is invalid");
                }


                _mapper.Map(invoiceDTO, invoice);
                _unitOfWork.Invoice.Update(invoice);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong in the {nameof(UpdateInvoice)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
    }
}
