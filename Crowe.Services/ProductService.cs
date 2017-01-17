using Crowe.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Crowe.Data.Infrastructure;
using Crowe.Domain;
namespace Crowe.Services
{
    public class ProductService
    {
        private readonly IProductRepository productsRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IProductRepository productsRepository, UnitOfWork unitOfWork)
        {
            this.productsRepository = productsRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Product> GetProducts(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return productsRepository.GetAll();
            }
            else
            {
                return productsRepository.GetAll().Where(p => p.Name == name);
            }
        }

        public Product GetProduct(int id)
        {
            var blog = productsRepository.GetById(id);
            return blog;
        }

        public Product GetProduct(string name)
        {
            var blog = productsRepository.GetProductByName(name);
            return blog;
        }

        public void CreateProduct(Product blog)
        {
            productsRepository.Add(blog);
        }

        public void UpdateProduct(Product blog)
        {
            productsRepository.Update(blog);
        }

        public void DeleteProduct(Product blog)
        {
            productsRepository.Delete(blog);
        }

        public void SaveProduct()
        {
            unitOfWork.Commit();
        }



    }
}
