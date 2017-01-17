using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crowe.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        CroweEntities dbContext;

        public CroweEntities Init()
        {
            return dbContext ?? (dbContext = new CroweEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
