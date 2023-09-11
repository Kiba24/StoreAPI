using AutoMapper;
using Core.Dtos.Brand;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        
        [HttpGet("GetAllBrands")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Brand> brandEntities = await _unitOfWork.BrandRepository.GetAllAsync();
            if (brandEntities.Any())
            {
                IEnumerable<BrandDto> dtos = brandEntities.Select(entity => _mapper.Map<BrandDto>(entity));
                return Ok(dtos);
            }
            return BadRequest("The list of brands is empty");

        }

        [HttpGet("GetBrand/{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            Brand brand = await _unitOfWork.BrandRepository.GetByIdAsync(id);
            if (brand == null) return NotFound();
            return Ok(_mapper.Map<BrandDto>(brand));
        }

        [HttpPut("UpdateBrand")]
        public async Task<IActionResult> UpdateBrand(BrandDto branddto)
        {
            Brand entity = _mapper.Map<Brand>(branddto);
          
            _unitOfWork.BrandRepository.Update(entity);
            _unitOfWork.Save();
            if (branddto == null) return BadRequest();
            return Ok(entity);

        }
        
        [HttpPost("SaveBrand")]
        public async Task<IActionResult> SaveBrand(BrandDto brandDto)
        {
            Brand entity = _mapper.Map<Brand>(brandDto);
            _unitOfWork.BrandRepository.Add(entity);
            _unitOfWork.Save();
            if (brandDto == null) return BadRequest();
            return CreatedAtAction(nameof(SaveBrand), new { id = brandDto.Id }, entity);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            Brand entity = await _unitOfWork.BrandRepository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            _unitOfWork.BrandRepository.Remove(entity);
            _unitOfWork.Save();
            return Ok("Product removed successfully");
        }
    }
}
