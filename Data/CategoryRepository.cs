using Data.Models;
using System.Collections.Generic;

namespace Data
{
    public class CategoryRepository : MongoRepository<Category>
    {
        public CategoryRepository()
        {
            SetCollection("categories");
        }
        public override void Update(Category entity)
        {
            Update(i => i.Id == entity.Id,
                new KeyValuePair<string, object>("Name", entity.Name),
                new KeyValuePair<string, object>("Description", entity.Description)
                );
        }
    }
}
