﻿using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(HttpClient Client) : base(Client, "api/products") { }

        public IEnumerable<Section> GetSections()
        {
            var sections = Get<IEnumerable<SectionDTO>>($"{Address}/sections");
            return sections.FromDTO();
        }

        public Section GetSection(int id)
        {
            var section = Get<SectionDTO>($"{Address}/sections/{id}");
            return section.FromDTO();
        }

        public IEnumerable<Brand> GetBrands()
        {
            var brands = Get<IEnumerable<BrandDTO>>($"{Address}/brands");
            return brands.FromDTO();
        }

        public Brand GetBrand(int id)
        {
            var brand = Get<BrandDTO>($"{Address}/brands/{id}");
            return brand.FromDTO();
        }

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            var response = Post(Address, Filter ?? new());
            var products = response.Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>().Result;
            return products.FromDTO();
        }

        public Product GetProductById(int Id)
        {
            var product = Get<ProductDTO>($"{Address}/{Id}");
            return product.FromDTO();
        }

    }
}