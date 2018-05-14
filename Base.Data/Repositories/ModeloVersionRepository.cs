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

    public class ModeloVersionRepository : AdoNetRepository<ModeloVersion>, IModeloVersionRepository
    {
        public ModeloVersionRepository(IAdoNetDbFactory dbFactory, IUnitOfWork uow) : base(dbFactory, uow)
        {

        }
    }

    public interface IModeloVersionRepository : IRepository<ModeloVersion>
    {

    }
}
