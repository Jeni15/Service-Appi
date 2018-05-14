﻿using Base.Data.Extensions;
using Base.Data.Infrastructure;
using Base.Data.Xml;
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

    public class ParametroRepository : AdoNetRepository<Parametro>, IParametroRepository
    {
        public ParametroRepository(IAdoNetDbFactory dbFactory, IUnitOfWork uow) : base(dbFactory,uow)
        {
           
        }
    }

    public interface IParametroRepository : IRepository<Parametro>
    {

    }
}
