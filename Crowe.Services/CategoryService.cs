using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crowe.Data.Infrastructure;
using Crowe.Domain;
using Crowe.Data.Infrastructure;
using Crowe.Data.Repositories;

namespace Crowe.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoriesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoriesRepository, IUnitOfWork unitOfWork)
        {
            _categoriesRepository = categoriesRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetCategories(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                return _categoriesRepository.GetAll();
            else
                return _categoriesRepository.GetAll().Where(c => c.Name.ToLower().Contains(name.ToLower()));
        }

        public Category GetCategory(int id)
        {
            var category = _categoriesRepository.GetById(id);
            return category;
        }
        
        public Category GetCategory(string name)
        {
            var category = _categoriesRepository.GetCategoryByName(name);
            return category;
        }

        public void CreateCategory(Category category)
        {
            _categoriesRepository.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            _categoriesRepository.Update(category);
        }

        public void DeleteCategory(Category category)
        {
            _categoriesRepository.Delete(category);
        }

        public void SaveCategory()
        {
            _unitOfWork.Commit();
        }


    }
}
