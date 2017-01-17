using System; 
using System.Collections; 
using System.Collections.Generic; 
using System.IO; 
using System.Linq; 
using System.Net; 
using System.Net.Http; 
using System.Net.Http.Formatting; 
using System.Net.Http.Headers; 
using System.Text; 
using System.Threading.Tasks; 
using Crowe.Domain; 


namespace CroweWebAPI.MediaTypeFormatters
{
    public class CategoryFormatter : BufferedMediaTypeFormatter
    {
        public CategoryFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/category"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }
    }
}