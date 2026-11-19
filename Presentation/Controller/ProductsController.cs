using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using ServicesAbstractions;
using Shared;
using Shared.DataTransferDto.ProductModulesDto;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {   //Get All Products
        [HttpGet]
        [Cache]
        public async Task<ActionResult<PaginationRespons<ProductDto>>> GetAllProduct([FromQuery]ProductQueryParameter queryParameters)
        {
            var products =await _serviceManager.productService.GetAllProductsAsync(queryParameters);
            return Ok(products);
        }

        //Get Product By Id
        [HttpGet("{id:int}")]
        [Cache]
        public async Task<ActionResult<IEnumerable <ProductDto>>> GetProductById(int id)
        {
           var Product =await _serviceManager.productService.GetProductByIdAsync(id);
            return Ok(Product);
        }

        //Get types

        [HttpGet("types")]
        [Cache]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await _serviceManager.productService.GetAllTybesAsync();
            return Ok(types);
        }

        //Get Brands
        [HttpGet("brands")]
        [Cache]
        public async Task<ActionResult<IEnumerable<BrandsDto>>> GetAllBrands()
        {
            var brands = await _serviceManager.productService.GetAllBrandsAsync();
            return Ok(brands);
        }

    }
}
