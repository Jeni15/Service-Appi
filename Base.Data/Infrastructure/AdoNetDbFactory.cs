using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.Infrastructure
{
    public class AdoNetDbFactory : Disposable, IAdoNetDbFactory
    {
        private AdoNetDbContext _dbContext;

        AdoNetDbContext IAdoNetDbFactory.Init()
        {
            return _dbContext ?? (_dbContext = new AdoNetDbContext());
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null) _dbContext.Dispose();
        }

    }
}
