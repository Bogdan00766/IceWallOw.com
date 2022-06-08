using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using IceWallOw.Application.Dto;
using IceWallOw.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> AddNewProductAsync(CreateProductDto newProduct)
        {
            if (string.IsNullOrEmpty(newProduct.Name))
            {
                throw new Exception("Product cannot have an empty Name");
            }

            //var product = _mapper.Map<Product>(newProduct);
            Product product = new Product()
            {
                Name = newProduct.Name,
                Price = newProduct.Price,
                Location = newProduct.Location,
                Description = newProduct.Description,
                Category = await _categoryRepository.FindByNameAsync(newProduct.CategoryName),
                PhotoPath = "NOT IMPLEMENTED XD"
            };

            if(product.Category == null)
            {
                throw new Exception("Category does not exist");
            }

            var createdProduct = _productRepository.Create(product);
            _productRepository.SaveAsync();
            return _mapper.Map<ProductDto>(createdProduct);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var posts = await _productRepository.FindAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(posts);
            //return posts.Select(product => new ProductDto
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    PhotoPath = product.PhotoPath,
            //    Description = product.Description,
            //    Price = product.Price,
            //    Location = product.Location,
            //    CategoryName = product.Category.Name,
            //});
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _productRepository.FindByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
            
            //return new ProductDto()
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    PhotoPath = product.PhotoPath,
            //    Description = product.Description,
            //    Price = product.Price,
            //    Location = product.Location,
            //    CategoryName = product.Category.Name,
            //};
        }
    }
}
