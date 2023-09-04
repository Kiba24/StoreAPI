using AutoMapper;
using Core.Dtos.Product;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Unit_Of_Work;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Product product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null) return NotFound();

            return Ok(_mapper.Map<ProductDto>(product));

        }

        [HttpGet("/Get")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Product> entities = await _unitOfWork.ProductRepository.GetAllAsync();
            if (entities == null) return NotFound();

            IEnumerable<ProductDto> dtos = entities.Select(entity => _mapper.Map<ProductDto>(entity));

            return Ok(dtos);

        }

        [HttpPost("SaveProduct")]
        public async Task<IActionResult> Save(AddProductDto product)
        {
            //I need to map the current dto to the entity.
            return Ok();
        }

    }
}
