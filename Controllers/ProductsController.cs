using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Interfaces;
using eCommerce.Core.Services;
using eCommerce.EF.Service;
using eCommerce.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _unitOfWork.ProductRopositry
                .GetAllAsync(x => x.Category, x => x.Photos);
            var result = _mapper.Map<List<ProductDTO>>(products);
            if (products == null)
                return BadRequest(new ResponseAPI(400, "No products found."));
            return Ok(result);
        }


        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _unitOfWork.ProductRopositry
                .GetByIdAsync(id, x => x.Category, x => x.Photos);
            if (product == null)
                return BadRequest(new ResponseAPI(400, $"Product with ID {id} not found."));
            var result = _mapper.Map<ProductDTO>(product);
            return Ok(result);
        }



        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] AddProductDTO productDTO)
        {
            var result = await _unitOfWork.ProductRopositry.AddAsync(productDTO);
            return Ok(new ResponseAPI(200));
        }



        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDTO updateProductDTO)
        {
            await _unitOfWork.ProductRopositry.UpdateAsync(updateProductDTO);
            return Ok(new ResponseAPI(200));
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.ProductRopositry.GetByIdAsync(id);
            if (product == null)
                return BadRequest(new ResponseAPI(400, $"Product with ID {id} not found."));

            await _unitOfWork.ProductRopositry.DeleteAsync(product);
            return Ok(new ResponseAPI(200));
        }



    }
}
