using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crowe.Data.Infrastructure;
using Crowe.Domain;

namespace Crowe.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public Product GetProductByName(string name)
        {
            var _product = this.DbContext.Products.Where(b => b.Name == name).FirstOrDefault();
            return _product;
            
        }

        public interface IProductRepository : IRepository<Product>
        {
            Product GetProductbyName(string name);
        }        
    }

    public interface IProductRepository : IRepository<Product>
    {
        Product GetProductByName(string name);
    }
}
