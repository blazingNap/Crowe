using Moq; 
using NUnit.Framework; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Net; 
using System.Net.Http; 
using System.Text; 
using System.Threading.Tasks; 
using System.Web;
using System.Web.Http; 
using System.Web.Http.Results; 
using System.Web.Http.Routing;
using CroweWebAPI.Controllers;
using Crowe.Data; 
using Crowe.Data.Infrastructure; 
using Crowe.Data.Configurations;
using Crowe.Data.Repositories; 
using Crowe.Domain; 
using Crowe.Services; 

namespace UnitTestingWebAPI.Tests 
{ 
    [TestFixture] 
    public class ControllerTests 
    { 
       #region Variables 
       ICategoryService _categoryService; 
       ICategoryRepository _categoryRepository; 
       IUnitOfWork _unitOfWork; 
       List<Category> _categories; 
       #endregion 
 
       #region Setup 
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
  
            foreach (Category _categorie in _categories) 
                 _categorie.ID = ++_counter; 
 
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
                     newCategory.CreatedBy = "wbaker";
                     newCategory.CreatedDate = DateTime.Now; 
                     _categories.Add(newCategory); 
                 })); 
  
             repo.Setup(r => r.Update(It.IsAny<Category>())) 
                 .Callback(new Action<Category>(x => 
               { 
                    var oldCategory = _categories.Find(a => a.ID == x.ID); 
                   oldCategory.Name = x.Name;
                   oldCategory.IsActive = true;
                   oldCategory.CreatedBy = x.CreatedBy;
                   oldCategory.CreatedDate = x.CreatedDate;
               })); 
 
            repo.Setup(r => r.Delete(It.IsAny<Category>())) 
                 .Callback(new Action<Category>(x => 
                 { 
                     var _categorieToRemove = _categories.Find(a => a.ID == x.ID); 
  
                     if (_categorieToRemove != null) 
                         _categories.Remove(_categorieToRemove); 
                 })); 
 
             // Return mock implementation 
             return repo.Object; 
         } 
 
         #endregion 
 
        [Test] 
        public void ControllerShouldReturnAllCategories() 
        { 
            var _categoryController = new CategoryController(_categoryService); 

            var result = _categoryController.GetCategories(); 
  
            CollectionAssert.AreEqual(result, _categories); 
        } 
  
       [Test] 
       public void ControlerShouldPutReturnBadRequestResult() 
       { 
          var _categoriesController = new CategoryController(_categoryService) 
          { 
               Configuration = new HttpConfiguration(), 
               Request = new HttpRequestMessage 
               { 
                  Method = HttpMethod.Put, 
                  RequestUri = new Uri("http://localhost/api/category/-1") 
               } 
           }; 
 
             var badresult = _categoriesController.PutCategory(-1, new Category() { Name = "Unknown Category" }); 
           Assert.That(badresult, Is.TypeOf<BadRequestResult>()); 
         } 
 
         [Test] 
         public void ControlerShouldPutUpdateFirstCategory() 
         { 
            var _categoriesController = new CategoryController(_categoryService) 
            { 
               Configuration = new HttpConfiguration(), 
               Request = new HttpRequestMessage 
               { 
                  Method = HttpMethod.Put, 
                  RequestUri = new Uri("http://localhost/api/articles/1") 
               } 
            }; 
 
           IHttpActionResult updateResult = _categoriesController.PutCategory(1, new Category() 
           { 
             ID = 1, 
             Name = "Bottle Water", 
             IsActive = true,
             Products = null,
             CreatedBy = "wbaker",
             CreatedDate = DateTime.UtcNow
             
             }) as IHttpActionResult; 
  
             Assert.That(updateResult, Is.TypeOf<StatusCodeResult>()); 

             StatusCodeResult statusCodeResult = updateResult as StatusCodeResult; 
 
             Assert.That(statusCodeResult.StatusCode, Is.EqualTo(HttpStatusCode.NoContent)); 
  
             Assert.That(_categories.First().Name, Is.EqualTo("Pop")); 
         } 
     } 
 } 
 





 

   






