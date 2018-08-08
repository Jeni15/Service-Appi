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
   
    public class UserTokenService : EntityService<UserToken>, IUserTokenService
    {
        public UserTokenService(IUnitOfWork unitOfWork, IUserTokenRepository repository) : base(unitOfWork, repository)
        {

        }

      

    }

    public interface IUserTokenService : IEntityService<UserToken>
    {

       
    }
}
