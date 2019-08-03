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
   
    public class CertificadoConsumoMasivoService : EntityService<CertificadoConsumoMasivo>, ICertificadoConsumoMasivoService
    {
        public CertificadoConsumoMasivoService(IUnitOfWork unitOfWork, ICertificadoConsumoMasivoRepository repository) : base(unitOfWork, repository)
        {

        }

      

    }

    public interface ICertificadoConsumoMasivoService : IEntityService<CertificadoConsumoMasivo>
    {

       
    }
}
