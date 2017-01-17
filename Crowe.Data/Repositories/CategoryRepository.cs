using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crowe.Data.Infrastructure;
using Crowe.Domain;

namespace Crowe.Data.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public Category GetCategoryByName(string name)
        {
            var _category = this.DbContext.Categories.Where(c => c.Name == name).FirstOrDefault();
            return _category;
        }
    }

    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetCategoryByName(string name);
    }
}
