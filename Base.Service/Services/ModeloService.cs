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
   
    public class ModeloService : EntityService<Modelo>, IModeloService
    {
        public ModeloService(IUnitOfWork unitOfWork, IModeloRepository repository) : base(unitOfWork, repository)
        {

        }

    }

    public interface IModeloService : IEntityService<Modelo>
    {
   
    }
}
