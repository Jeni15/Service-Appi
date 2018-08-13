using Base.Data.Extensions;
using Base.Data.Infrastructure;
using Base.Data.Xml;
using Base.Model.Dtos;
using Base.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.Repositories
{

    public class TipoFallaRepository : AdoNetRepository<TipoFalla>, ITipoFallaRepository
    {
        public TipoFallaRepository(IAdoNetDbFactory dbFactory, IUnitOfWork uow) : base(dbFactory,uow)
        {
           
        }

    }

    public interface ITipoFallaRepository : IRepository<TipoFalla>
    {
   
    }
}
