using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crowe.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Crowe.Data.Configurations
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            ToTable("Category");
            Property(a => a.Name).IsRequired().HasMaxLength(50);
            Property(a => a.IsActive).IsRequired();
        }
    }
}
