using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crowe.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Crowe.Data.Configurations
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("Product");
            Property(a => a.Name).IsRequired().HasMaxLength(50);
            Property(a => a.Description).HasMaxLength(50);
            Property(a => a.IsActive).IsRequired();
            Property(a => a.Quantity).IsRequired();
        }
    }
}
