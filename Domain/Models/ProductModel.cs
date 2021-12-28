using Data.Models;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Domain.Models
{
    public class ProductModel : MongoModelBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public string CategoryId { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }

        public CategoryModel Category { get; set; }

        public static implicit operator ProductModel(Product product)
        {
            if (product == null)
                return null;
            return new ProductModel
            {
                Id = product.Id != ObjectId.Empty ? product.Id.ToString() : null,
                //Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                //CategoryId = product.CategoryId != ObjectId.Empty ? product.CategoryId.ToString() : null,
                //Category = new CategoryModel { Id = product.CategoryId != ObjectId.Empty ? product.CategoryId.ToString() : null },
                Price = product.Price,
                Currency = product.Currency
            };
        }

        public static implicit operator Product(ProductModel product)
        {
            if (product == null)
                return null;
            return new Product
            {
                Id = product.Id != null ? new ObjectId(product.Id) : ObjectId.Empty,
                //Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                //CategoryId = product.CategoryId != null ? new ObjectId(product.CategoryId) : ObjectId.Empty,
                Price = product.Price,
                Currency = product.Currency
            };
        }
    }
}
