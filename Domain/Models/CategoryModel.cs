using Data.Models;
using MongoDB.Bson;

namespace Domain.Models
{
    public class CategoryModel : MongoModelBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public static implicit operator CategoryModel(Category category)
        {
            if (category == null)
                return null;
            return new CategoryModel
            {
                //Id = Convert.ToString(category.Id),
                Id = category.Id != ObjectId.Empty ? category.Id.ToString() : null,
                //Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public static implicit operator Category(CategoryModel category)
        {
            if (category == null)
                return null;
            return new Category
            {
                //Id = new MongoDB.Bson.ObjectId(product.Id),
                Id = category.Id != null ? new ObjectId(category.Id) : ObjectId.Empty,
                //Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
        }
    }
}