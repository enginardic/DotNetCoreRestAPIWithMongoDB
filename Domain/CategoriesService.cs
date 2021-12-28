using Data;
using Domain.Models;
using System;

namespace Domain
{
    public class CategoriesService : IDomainService
    {
        private readonly CategoryRepository categoryRepository = new CategoryRepository();

        public CategoryModel Get(string id, out Result result)
        {
            result = null;

            if (string.IsNullOrEmpty(id))
            {
                result = DefaultResults.InvalidRequest;
                return null;
            }

            CategoryModel category = categoryRepository.Get(id);

            if (category is null)
            {
                result = DefaultResults.NotFoundResult;
                return null;
            }

            return category;
        }

        public void Add(CategoryModel category, out Result result)
        {
            result = null;

            if (category is null)
            {
                result = DefaultResults.InvalidRequest;
                return;
            }

            var insertedCategory = categoryRepository.Insert(category);
            category.Id = insertedCategory.Id.ToString();
        }

        public void Update(string id, CategoryModel categoryUpdate, out Result result)
        {
            result = null;

            if (string.IsNullOrEmpty(id) || categoryUpdate is null)
            {
                result = DefaultResults.InvalidRequest;
                return;
            }

            var category = categoryRepository.Get(id);
            if (category is null)
            {
                result = DefaultResults.NotFoundResult;
                return;
            }

            category.Name = categoryUpdate.Name;
            category.Description = categoryUpdate.Description;

            categoryRepository.Update(category);
        }

        public void Delete(string id, out Result result)
        {
            result = null;

            if (string.IsNullOrEmpty(id))
            {
                result = DefaultResults.InvalidRequest;
                return;
            }

            var category = categoryRepository.Get(id);
            if (category is null)
            {
                result = DefaultResults.NotFoundResult;
                return;
            }

            categoryRepository.Delete(id);
        }
    }
}
