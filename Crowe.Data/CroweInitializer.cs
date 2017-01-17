using System; 
using System.Collections.Generic; 
using System.Data.Entity; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks;
using Crowe.Domain;

namespace Crowe.Data
{
    public class CroweInitializer : DropCreateDatabaseIfModelChanges<CroweEntities>
    {
        protected override void Seed(CroweEntities context)
        {
            GetCategories().ForEach(c => context.Categories.Add(c));
            GetWaterProducts().ForEach(w => context.Products.Add(w));
            GetPopProducts().ForEach(p => context.Products.Add(p));
            context.Commit();
        }

        public static List<Category> GetCategories()
        {
            List<Category> _categories = new List<Category>();

            Category _category1 = new Category()
            {
                Name = "Bottled Water",
                IsActive = true,
                Products = GetWaterProducts(),
                CreatedBy = "wbaker",
                CreatedDate = DateTime.UtcNow
            };

            Category _category2 = new Category()
            {
                Name = "Pop",
                IsActive = true,
                Products = GetPopProducts(),
                CreatedBy = "wbaker",
                CreatedDate = DateTime.UtcNow
            };

            _categories.Add(_category1);
            _categories.Add(_category2);
            return _categories;
        }

        public static List<Product> GetWaterProducts()
        {
            List<Product> _bottledWaters = new List<Product>();
            
            Product bottledWater1 = new Product()
            {
                Name = "Ice Mountain",
                Description = "Ice Mountain Bottled Water",
                IsActive = true,
                Quantity = 1,
                CategoryID = 1
            };

            Product bottledWater2 = new Product()
            {
                Name = "Dasani",
                Description = "Dasani Bottled Sprint Water",
                IsActive = true,
                Quantity = 1,
                CategoryID = 1
            };

            Product bottledWater3 = new Product()
            {
                Name = "Evian",
                Description = "Evian Bottled Water",
                IsActive = true,
                Quantity = 1,
                CategoryID = 1
            };

            Product bottledWater4 = new Product()
            {
                Name = "Nestle",
                Description = "Evian Bottled Water",
                IsActive = true,
                Quantity = 1,
                CategoryID = 1
            };
            
            _bottledWaters.Add(bottledWater1);
            _bottledWaters.Add(bottledWater2);
            _bottledWaters.Add(bottledWater3);
            _bottledWaters.Add(bottledWater4);
      
            return _bottledWaters;
        }

        public static List<Product> GetPopProducts()
        {
            List<Product> _pops = new List<Product>();
            
            Product pop1 = new Product()
            {
                Name = "Sunkist",
                Description = "Orange Sunkist",
                IsActive = true,
                Quantity = 1,
                CategoryID = 2
            };

            Product pop2 = new Product()
            {
                Name = "Sprite",
                Description = "Sprite Clear Pop",
                IsActive = true,
                Quantity = 1,
                CategoryID = 2
            };

            Product pop3 = new Product()
            {
                Name = "Pepsi",
                Description = "Pepsi Dark Pop",
                IsActive = true,
                Quantity = 1,
                CategoryID = 2
            };

            Product pop4 = new Product()
            {
                Name = "Coco Cola",
                Description = "Coco Cola Dark Pop",
                IsActive = true,
                Quantity = 1,
                CategoryID = 2
            };
            
            _pops.Add(pop1);
            _pops.Add(pop2);
            _pops.Add(pop3);
            _pops.Add(pop4);
      
            return _pops;
        }      
    }
}
