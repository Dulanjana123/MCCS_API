using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.IRepository;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CustomerController> _logger;
        private readonly IMapper _mapper;

        public CustomerController(IUnitOfWork unitOfWork, ILogger<CustomerController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var customers = await _unitOfWork.Customer.GetAll();

                var results = _mapper.Map<IList<CustomerDTO>>(customers);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetCustomers)}");
                return StatusCode(500, $"Internal Server Error. Please Try Again Later. {ex}");
            }
        }

        [HttpGet("{id:int}", Name = "GetCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _unitOfWork.Customer.Get(x => x.Id == id);
            var result = _mapper.Map<CustomerDTO>(customer);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveCustomer([FromBody] CustomerDTO customerDTO)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in the {nameof(SaveCustomer)}");
                return BadRequest(ModelState);
            }

            else
            {
                try
                {
                    var customer = _mapper.Map<Customer>(customerDTO);
                    customer.IsActive = true;
                    await _unitOfWork.Customer.Insert(customer);
                    await _unitOfWork.Save();
                    return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Something Went Wrong in the {nameof(SaveCustomer)}");
                    return StatusCode(500, "Internal Server Error. Please Try Again Later.");
                }
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid POST attempt in the {nameof(UpdateCustomer)}");
                return BadRequest(ModelState);
            }

            try
            {
                var customer = await _unitOfWork.Customer.Get(x => x.Id == id);
                if (customer == null)
                {
                    _logger.LogError($"Invalid Update attempt in the {nameof(UpdateCustomer)}");
                    return BadRequest("Submited data is invalid");
                }


                _mapper.Map(customerDTO, customer);
                _unitOfWork.Customer.Update(customer);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong in the {nameof(UpdateCustomer)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
    }
}
