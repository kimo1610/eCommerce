using AutoMapper;
using Azure.Messaging;
using eCommerce.Core.DTO.CategoryDTO;
using eCommerce.Core.Entities.Product;
using eCommerce.Core.Interfaces;
using eCommerce.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoriesController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _unitOfWork.CategoryRopositry.GetAllAsync();
            if (categories == null || !categories.Any())
                return BadRequest(new ResponseAPI(400, "No categories found."));

            return Ok(categories);
        }



        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _unitOfWork.CategoryRopositry.GetByIdAsync(id);

            if (category == null)
                return BadRequest(new ResponseAPI(400, $"Category with ID {id} not found."));

            return Ok(category);
            
        }





        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(AddCategoryDTO addCategoryDTO)
        {
            var category = _mapper.Map<Category>(addCategoryDTO);
            var result = await _unitOfWork.CategoryRopositry.AddAsync(category);

            if (result == null)
                return BadRequest(new ResponseAPI(400, "Failed to add category."));

            return Ok(new ResponseAPI(200, "Category added successfully."));
        }



        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDTO )
        {

            var category = _mapper.Map<Category>(updateCategoryDTO);

             var result = await _unitOfWork.CategoryRopositry.UpdateAsync(category);

            if (result == null)
                return BadRequest(new ResponseAPI(400, "Failed to update category."));

            return Ok(new ResponseAPI(200, "Category updated successfully."));
        }



        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _unitOfWork.CategoryRopositry.GetByIdAsync(id);
            if (category == null)
                return BadRequest(new ResponseAPI(400, $"Category with ID {id} not found."));

            var result = await _unitOfWork.CategoryRopositry.DeleteAsync(id);
            if (result == null)
                return BadRequest(new ResponseAPI(400, "Failed to delete category."));

            return Ok(new ResponseAPI(200, "Category deleted successfully."));
        }




    }
}
