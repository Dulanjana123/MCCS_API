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
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, ILogger<ProductController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _unitOfWork.Product.GetAll();

                var results = _mapper.Map<IList<ProductDTO>>(products);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetProducts)}");
                return StatusCode(500, $"Internal Server Error. Please Try Again Later. {ex}");
            }
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _unitOfWork.Product.Get(x => x.Id == id);
            var result = _mapper.Map<ProductDTO>(product);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveProduct([FromBody] ProductDTO productDTO)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in the {nameof(SaveProduct)}");
                return BadRequest(ModelState);
            }

            else
            {
                try
                {
                    var product = _mapper.Map<Product>(productDTO);
                    product.IsActive = true;
                    await _unitOfWork.Product.Insert(product);
                    await _unitOfWork.Save();
                    return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Something Went Wrong in the {nameof(SaveProduct)}");
                    return StatusCode(500, "Internal Server Error. Please Try Again Later.");
                }
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid POST attempt in the {nameof(UpdateProduct)}");
                return BadRequest(ModelState);
            }

            try
            {
                var product = await _unitOfWork.Product.Get(x => x.Id == id);
                if (product == null)
                {
                    _logger.LogError($"Invalid Update attempt in the {nameof(UpdateProduct)}");
                    return BadRequest("Submited data is invalid");
                }


                _mapper.Map(productDTO, product);
                _unitOfWork.Product.Update(product);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong in the {nameof(UpdateProduct)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
    }
}
