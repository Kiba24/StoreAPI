using Core.Dtos.Product;
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

        public ProductController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _unitOfWork.ProductRepository.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.ProductRepository.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Save(AddProductDto product)
        {
            //I need to map the current dto to the entity.
            return Ok(_unitOfWork.ProductRepository.Add());
        }

    }
}
