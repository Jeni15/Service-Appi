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
   
    public class VersionService : EntityService<Model.Models.Version>, IVersionService
    {
        public VersionService(IUnitOfWork unitOfWork, IModeloVersionRepository repository) : base(unitOfWork, repository)
        {

        }

    }

    public interface IVersionService : IEntityService<Model.Models.Version>
    {
   
    }
}
