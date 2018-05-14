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
   
    public class ParametroService : EntityService<Parametro>, IParametroService
    {
        public ParametroService(IUnitOfWork unitOfWork, IParametroRepository repository) : base(unitOfWork, repository)
        {

        }

    }

    public interface IParametroService : IEntityService<Parametro>
    {
   
    }
}
