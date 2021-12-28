using Data.Models;
using System.Collections.Generic;

namespace Data
{
    public class ProductRepository : MongoRepository<Product>
    {
        public ProductRepository()
        {
            SetCollection("products");
        }
        public override void Update(Product entity)
        {
            Update(i => i.Id == entity.Id,
                new KeyValuePair<string, object>("Name", entity.Name),
                new KeyValuePair<string, object>("Description", entity.Description),
                new KeyValuePair<string, object>("CategoryId", entity.CategoryId),
                new KeyValuePair<string, object>("Price", entity.Price),
                new KeyValuePair<string, object>("Currency", entity.Currency)
                );
        }
    }
}
