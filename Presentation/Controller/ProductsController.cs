using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.DataTransferDto;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {   //Get All Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProduct()
        {
            var products =await _serviceManager.productService.GetAllProductsAsync();
            return Ok(products);
        }

        //Get Product By Id
        [HttpGet("{id:int}")]

        public async Task<ActionResult<IEnumerable <ProductDto>>> GetProductById(int id)
        {
           var Product =await _serviceManager.productService.GetProductByIdAsync(id);
            return Ok(Product);
        }

        //Get types

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await _serviceManager.productService.GetAllTybesAsync();
            return Ok(types);
        }

        //Get Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandsDto>>> GetAllBrands()
        {
            var brands = await _serviceManager.productService.GetAllBrandsAsync();
            return Ok(brands);
        }

    }
}
