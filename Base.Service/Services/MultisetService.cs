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
   
    public class MultisetService : EntityService<Multiset>, IMultisetService
    {
        public MultisetService(IUnitOfWork unitOfWork, IMultisetRepository repository) : base(unitOfWork, repository)
        {

        }

    }

    public interface IMultisetService : IEntityService<Multiset>
    {

    }
}
