using Base.Data.Infrastructure;
using Base.Data.Repositories;
using Base.Model.Dtos;
using Base.Model.Models;
using Base.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Service.Services
{
   
    public class LogConsultaService : EntityService<LogConsulta>, ILogConsultaService
    {
        public LogConsultaService(IUnitOfWork unitOfWork, ILogConsultaRepository repository) : base(unitOfWork, repository)
        {

        }

      

    }

    public interface ILogConsultaService : IEntityService<LogConsulta>
    {

       
    }
}
