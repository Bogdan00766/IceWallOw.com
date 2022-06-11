using DbInfrastructure;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace IceWallOw.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        [HttpGet(Name = "GetProducts")]
        public async Task<List<Product>> Get()
        {
            using(IUnitOfWork uow = new UnitOfWork())
            {
                return await uow.ProductRepository.FindAllAsync();
            }
            
        }
    }
}
