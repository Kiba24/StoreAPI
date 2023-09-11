using AutoMapper;
using Core.Dtos.Category;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCategories()
        {
            IEnumerable<Category> entities = await _unitOfWork.CategoryRepository.GetAllAsync();
            if (!entities.Any()) return BadRequest();
            IEnumerable<CategoryDto> dtos = entities.Select(entity => _mapper.Map<CategoryDto>(entity));
            return Ok(dtos);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            Category category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UppdateCategory(CategoryDto categoryDto)
        {
            Category entity = _mapper.Map<Category>(categoryDto);
            if (entity == null) return BadRequest();
            _unitOfWork.CategoryRepository.Update(entity);
            _unitOfWork.Save();
            return Ok(entity);

        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            Category entity = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            _unitOfWork.CategoryRepository.Remove(entity);
            _unitOfWork.Save();
            return Ok("Category removed successfully");
        }

        [HttpPost("SaveCategory")]
        public async Task<IActionResult> SaveCategory(CategoryDto categoryDto)
        {
            if (categoryDto == null) return BadRequest();
            Category entity = _mapper.Map<Category>(categoryDto);
            _unitOfWork.CategoryRepository.Add(entity);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(SaveCategory), new { id = categoryDto.Id } , entity);

        }
    }
}
