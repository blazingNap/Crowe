using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crowe.Data.Infrastructure;
using Crowe.Domain;
using System.Web.Http.Description;
using Crowe.Services;
using System.Web.ModelBinding;
using System.Data.Entity.Infrastructure;

namespace CroweWebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        } 

        // GET: api/Categories
        public IEnumerable<Category> GetCategories()
        {
            return _categoryService.GetCategories();
        }

        // GET: api/Categories
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = _categoryService.GetCategory(id);
            if(category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
        
        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.ID)
            {
                return BadRequest();
            }

            _categoryService.UpdateCategory(category);

            try
            {
                _categoryService.SaveCategory();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
                return StatusCode(HttpStatusCode.NoContent);
        }
 
        // POST: api/Category
        [ResponseType(typeof(Category))] 
      public IHttpActionResult PostCategory(Category category) 
      { 
          if (!ModelState.IsValid) 
          { 
              return BadRequest(ModelState); 
          } 

          _categoryService.CreateCategory(category); 
 
          return CreatedAtRoute("DefaultApi", new { id = category.ID }, category); 
      } 

        // DELETE: api/Categorys/5 
        [ResponseType(typeof(Category))] 
        public IHttpActionResult DeleteCategory(int id) 
        { 
            Category category = _categoryService.GetCategory(id); 
            if (category == null) 
            { 
                return NotFound(); 
            } 
 
            _categoryService.DeleteCategory(category); 

           return Ok(category); 
        } 

        private bool CategoryExists(int id) 
        { 
           return _categoryService.GetCategory(id) != null; 
        } 
   }
}
