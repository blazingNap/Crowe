using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crowe.Domain
{
    public class Product : BaseModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int Quantity { get; set; }
        
        public int CategoryID { get; set; }
        
        //Navigation Property
        public virtual Category Category { get; set; }

        public Product()
        {
        }
    }
}
