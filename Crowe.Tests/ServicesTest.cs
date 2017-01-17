using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crowe.Services;
using Crowe.Data;
using Crowe.Data.Repositories;
using Crowe.Domain;
using Crowe.Data.Infrastructure;
using NUnit;
using NUnit.Framework;

namespace Crowe.Tests
{
    public class ServicesTest
    {
        ICategoryService _categoryService;
        ICategoryRepository _categoryRepository;
        IUnitOfWork _unitOfWork;
        List<Category> _categories;

        [SetUp]
        public void Setup()
        {
            _categories = SetupCategories();
            _categoryRepository = SetupCategoryRepository();
            _unitOfWork = new Mock<IUnitOfWork>().Object;
            _categoryService = new CategoryService(_categoryRepository, _unitOfWork);
        }

        public List<Category> SetupCategories() 
        { 
            int _counter = new int(); 
            List<Category> _categories = CroweInitializer.GetCategories(); 

            foreach (Category _category in _categories) 
            _category.ID = ++_counter; 

           return _categories; 
        } 

        public ICategoryRepository SetupCategoryRepository() 
         { 
             // Init repository 
             var repo = new Mock<ICategoryRepository>(); 
 
 
             // Setup mocking behavior 
             repo.Setup(r => r.GetAll()).Returns(_categories); 
 
            repo.Setup(r => r.GetById(It.IsAny<int>())) 
                 .Returns(new Func<int, Category>( 
                     id => _categories.Find(a => a.ID.Equals(id)))); 
 
 
             repo.Setup(r => r.Add(It.IsAny<Category>())) 
                .Callback(new Action<Category>(newCategory => 
                 { 
                     dynamic maxCategoryID = _categories.Last().ID; 
                     dynamic nextCategoryID = maxCategoryID + 1; 
                     newCategory.ID = nextCategoryID; 
                     newCategory.CreatedDate = DateTime.Now; 
                     _categories.Add(newCategory); 
                 })); 
 
             repo.Setup(r => r.Update(It.IsAny<Category>())) 
                 .Callback(new Action<Category>(x => 
                     { 
                         var oldCategory = _categories.Find(a => a.ID == x.ID); 
                         oldCategory.ModifiedDate = DateTime.Now; 
                         oldCategory = x; 
                    })); 

             repo.Setup(r => r.Delete(It.IsAny<Category>())) 
                .Callback(new Action<Category>(x => 
                 { 
                     var _categoryToRemove = _categories.Find(a => a.ID == x.ID); 
 
                     if (_categoryToRemove != null) 
                        _categories.Remove(_categoryToRemove); 
                 })); 
 
             // Return mock implementation 
             return repo.Object; 
         } 

         [Test] 
         public void ServiceShouldReturnAllCategories() 
         {
             var categories = _categoryService.GetCategories();

             Assert.That(categories, Is.EqualTo(_categories)); 
         } 

         public void ServiceShouldReturnRightCategory() 
         { 
            var secondCategory = _categoryService.GetCategory(1);

            Assert.That(secondCategory, 
                Is.EqualTo(_categories.Find(a => a.Name.Contains("Sprite")))); 
         } 
    }
}
