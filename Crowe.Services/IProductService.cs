using Crowe.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crowe.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(string name = null);
        Product GetProduct(int id);
        Product GetProduct(string name);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void SaveProduct();
        void DeleteProduct(Product product);
    }
}
