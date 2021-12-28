using Data;
using Domain.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace Domain
{
    public class ProductsService : DomainServiceBase
    {
        private readonly ProductRepository productRepository = new ProductRepository();
        private readonly CategoryRepository categoryRepository = new CategoryRepository();

        public ProductModel Get(string id, out Result result)
        {
            result = null;

            if (string.IsNullOrEmpty(id))
            {
                result = DefaultResults.InvalidRequest;
                return null;
            }
            string productKey = "product-" + id;
            var product = GetCacheObject<ProductModel>(productKey);
            if (product != null)
                return product;

            product = productRepository.Get(id);

            if (product is null)
            {
                result = DefaultResults.NotFoundResult;
                return null;
            }

            product.Category = categoryRepository.Get(product.CategoryId);

            if (product.Category is null)
            {
                result = DefaultResults.NotFoundResult;
                return null;
            }
            SetCacheObject(productKey, product, 5);
            return product;
        }

        public void Add(ProductModel product, out Result result)
        {
            result = null;

            if (product is null)
            {
                result = DefaultResults.InvalidRequest;
                return;
            }

            var insertedProduct = productRepository.Insert(product);
            product.Id = insertedProduct.Id.ToString();
        }

        public void Update(string id, ProductModel productUpdate, out Result result)
        {
            result = null;

            if (string.IsNullOrEmpty(id) || productUpdate is null)
            {
                result = DefaultResults.InvalidRequest;
                return;
            }

            var product = productRepository.Get(id);
            if (product is null)
            {
                result = DefaultResults.NotFoundResult;
                return;
            }

            product.Name = productUpdate.Name;
            product.Description = productUpdate.Description;
            product.CategoryId = productUpdate.CategoryId;
            product.Price = productUpdate.Price;
            product.Currency = productUpdate.Currency;

            productRepository.Update(product);
        }

        public void Delete(string id, out Result result)
        {
            result = null;

            if (string.IsNullOrEmpty(id))
            {
                result = DefaultResults.InvalidRequest;
                return;
            }

            var product = productRepository.Get(id);
            if (product is null)
            {
                result = DefaultResults.NotFoundResult;
                return;
            }

            productRepository.Delete(id);
        }
    }
}
