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

        [SwaggerOperation(Summary = "Retrieves all products")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await _productService.GetAllProducts();
            return Ok(posts);
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
        //[HttpGet(Name = "GetProducts")]
        //public async Task<List<Product>> ReturnProductsGet(string? name = null, string? location = null, int? distance = null, int? categoryId = null, float? priceMin = null, float? priceMax = null)
        //{
        //    List<Product> output;
        //    using(IUnitOfWork uow = new UnitOfWork())
        //    {
        //        output = await uow.ProductRepository.FindAllAsync();
        //    }
        //    if (name != null) output = output.Where(x => x.Name.Contains(name)).ToList();
        //    if (categoryId != null) output = output.Where(x => x.Category.Id == categoryId).ToList();
        //    if (priceMin != null) output = output.Where(x => x.Price >= priceMin).ToList();
        //    if(priceMax != null) output = output.Where(x => x.Price <= priceMax).ToList();
        //    if(location != null)
        //    {
        //        int? dis;
        //        if (distance != null) dis = distance;
        //        else dis = 0;
        //        List<Product> tmp = new List<Product>();
        //        foreach(var product in output)
        //        {
        //            if (await IceWallOw.GoogleMaps.Requests.CalculateDistance(location, product.Location) <= dis) tmp.Add(product);
        //            //new Thread(async () =>
        //            //{
        //            //    //Thread.CurrentThread.IsBackground = true;
        //            //    if(await IceWallOw.GoogleMaps.Requests.CalculateDistance(location, product.location) <= dis) tmp.Add(product);
        //            //}).Start();
        //        }
        //        output = tmp;
        //    }
        //    return output;
        //}
    }
}
