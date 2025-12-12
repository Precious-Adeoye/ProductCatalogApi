using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.Application.DTOs.ProductDtos;
using ProductCatalog.Core.Contracts.Interface;
using ProductCatalog.Core.Entities;
using ProductCatalog.Infrastructure.IServices;

namespace ProductCatalog.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        public ProductService(IUnitOfWork uow) { _uow = uow; }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product { Name = dto.Name, Description = dto.Description, Price = dto.Price, StockQuantity = dto.StockQuantity, Sku = dto.Sku, CreatedAt = DateTime.UtcNow };
            await _uow.ProductRepository.AddAsync(product);
            return new ProductDto { Id = product.Id, Name = product.Name, Description = product.Description, Price = product.Price, StockQuantity = product.StockQuantity, Sku = product.Sku, CreatedAt = product.CreatedAt };
        }

        public async Task DeleteAsync(int id)
        {
            var p = await _uow.ProductRepository.GetByIdAsync(id);
            if (p != null) await _uow.ProductRepository.DeleteAsync(p);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var list = await _uow.ProductRepository.GetAllAsync();
            return list.Select(p => new ProductDto { Id = p.Id, Name = p.Name, Description = p.Description, Price = p.Price, StockQuantity = p.StockQuantity, Sku = p.Sku, CreatedAt = p.CreatedAt });
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var p = await _uow.ProductRepository.GetByIdAsync(id);
            if (p == null) return null;
            return new ProductDto { Id = p.Id, Name = p.Name, Description = p.Description, Price = p.Price, StockQuantity = p.StockQuantity, Sku = p.Sku, CreatedAt = p.CreatedAt };
        }

        public async Task UpdateAsync(int id, UpdateProductDto dto)
        {
            var p = await _uow.ProductRepository.GetByIdAsync(id);
            if (p == null) return;
            if (!string.IsNullOrEmpty(dto.Name)) p.Name = dto.Name;
            if (dto.Description != null) p.Description = dto.Description;
            if (dto.Price.HasValue) p.Price = dto.Price.Value;
            if (dto.StockQuantity.HasValue) p.StockQuantity = dto.StockQuantity.Value;
            p.UpdatedAt = DateTime.UtcNow;
            await _uow.ProductRepository.UpdateAsync(p);
        }
    }
}
