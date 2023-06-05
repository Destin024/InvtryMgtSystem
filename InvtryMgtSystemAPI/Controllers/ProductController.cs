using AutoMapper;
using InvtryMgtSystemAPI.Data.Dto;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetProduct()
        {
            var products = _mapper.Map<List<ProductDto>> (_productRepository.GetProducts());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(products);
        }
        [HttpGet("productId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult GetProduct(Guid productId)
        {
            var product = _mapper.Map<ProductDto>(_productRepository.GetProduct(productId));

            if (productId == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult CreateProduct([FromBody] ProductDto createProduct)
        {
            if (createProduct ==null)
            {
                return BadRequest(ModelState);
            }
            var product = _productRepository.GetProducts()
                .Where(p => p.Name.Trim().ToUpper() == createProduct.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (product != null)
            {
                ModelState.AddModelError("", "Product Already Exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productMap = _mapper.Map<Product>(createProduct);
            if (!_productRepository.CreateProduct(productMap))
            {
                ModelState.AddModelError("", "Something wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");

        }

        [HttpPut("{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult UpdateProduct(Guid productId,[FromBody]ProductDto updatedProduct)
        {
            if (updatedProduct == null)
            {
                BadRequest(ModelState);
            }
            if (productId == null)
            {
                return BadRequest(ModelState);
            }
            if (!_productRepository.ProductExists(productId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productMap = _mapper.Map<Product>(updatedProduct);

            if (!_productRepository.UpdateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating product");
            }
            return NoContent();
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteProduct(Guid productId)
        {
            if (!_productRepository.ProductExists(productId))
            {
                return NotFound();
            }
            var productToDelete = _productRepository.GetProduct(productId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_productRepository.DeleteProduct(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting product");
                return StatusCode(400, ModelState);
            }
            return NoContent();
        }
    }
}
