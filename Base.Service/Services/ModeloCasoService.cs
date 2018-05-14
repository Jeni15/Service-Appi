using Base.Data.Infrastructure;
using Base.Data.Repositories;
using Base.Model.Models;
using Base.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Service.Services
{
   
    public class ModeloCasoService : EntityService<ModeloCaso>, IModeloCasoService
    {
        public ModeloCasoService(IUnitOfWork unitOfWork, IModeloCasoRepository repository) : base(unitOfWork, repository)
        {

        }

    }

    public interface IModeloCasoService : IEntityService<ModeloCaso>
    {
   
    }
}
