using DbInfrastructure;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using IceWallOw.GoogleMaps;
using IceWallOw.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using IceWallOw.Application.Dto;

namespace IceWallOw.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [SwaggerOperation(Summary = "Retrieves all products with parameters")]
        [HttpGet]
        public async Task<IActionResult> Get(int page = 0, string? name = null, string? location = null, int? distance = null, string? categoryName = null, float? priceMin = null, float? priceMax = null)
        {
            var output = await _productService.GetAllProducts();



            if (name != null) output = output.Where(x => x.Name.Contains(name)).ToList();
            if (categoryName != null) output = output.Where(x => x.CategoryName == categoryName).ToList();
            if (priceMin != null) output = output.Where(x => x.Price >= priceMin).ToList();
            if (priceMax != null) output = output.Where(x => x.Price <= priceMax).ToList();
            if (location != null)
            {
                int? dis;
                if (distance != null) dis = distance;
                else dis = 0;
                List<ProductDto> tmp = new List<ProductDto>();
                foreach (var product in output)
                {
                    if (await IceWallOw.GoogleMaps.Requests.CalculateDistance(location, product.Location) <= dis) tmp.Add(product);
                    //new Thread(async () =>
                    //{
                    //    //Thread.CurrentThread.IsBackground = true;
                    //    if(await IceWallOw.GoogleMaps.Requests.CalculateDistance(location, product.location) <= dis) tmp.Add(product);
                    //}).Start();
                }
                output = tmp;
            }

            int pages = (int)Math.Ceiling((float)(output.Count() / 10));
            output = output.Skip(page*15).Take(15).ToList();
            //Dictionary<string, int> pagesCount = new Dictionary<string, int>();
            //pagesCount.Add("pagesCount", pages);
            //Dictionary<Dictionary<string, int>, IEnumerable<ProductDto>> dic = new Dictionary<Dictionary<string, int>, IEnumerable<ProductDto>>();
            //dic.Add(pagesCount, output);
            return Ok(output);
        }

        [SwaggerOperation(Summary = "Retrieves specific product by ID")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _productService.GetProductById(id);
            if(post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [SwaggerOperation(Summary = "Create a new product")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto newProduct)
        {
            var product = await _productService.AddNewProductAsync(newProduct);
            return Created($"api/products/{product.Id}", product);
        }
      
    }
}
