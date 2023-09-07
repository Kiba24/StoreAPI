using AutoMapper;
using Core.Dtos.Product;
using Core.Entities;
using Core.Interfaces;
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
        public async Task<IActionResult> Save(AddUpdateProductDto product)
        {
            Product entity = _mapper.Map<Product>(product);
            _unitOfWork.ProductRepository.Add(entity);
            _unitOfWork.Save();
            if (product == null) return BadRequest();
            return CreatedAtAction(nameof(Save),new {id = product.Id}, entity);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> Update(AddUpdateProductDto product)
        {
            Product entity = _mapper.Map<Product>(product);
            _unitOfWork.ProductRepository.Update(entity);
            _unitOfWork.Save();
            if (product == null) return BadRequest();
            return Ok(entity);

        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (await _unitOfWork.ProductRepository.GetByIdAsync(id) == null) return NotFound();
            _unitOfWork.ProductRepository.Remove(await _unitOfWork.ProductRepository.GetByIdAsync(id));
            _unitOfWork.Save();
            return Ok("Product removed successfuly");
            

        }
    }
}
