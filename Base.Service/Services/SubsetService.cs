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
   
    public class SubsetService : EntityService<Subset>, ISubsetService
    {
        public SubsetService(IUnitOfWork unitOfWork, ISubsetRepository repository) : base(unitOfWork, repository)
        {

        }

    }

    public interface ISubsetService : IEntityService<Subset>
    {

    }
}
