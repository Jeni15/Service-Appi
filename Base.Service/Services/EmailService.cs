using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Base.Service.Services
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Conecte su servicio de correo electrónico aquí para enviar un correo electrónico.
            return Task.FromResult(0);
        }
    }
}
