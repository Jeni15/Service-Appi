using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Models
{
    public class ClienteFormViewModel
    {
        public Cliente Cliente { get; set; }

        public IEnumerable<TipoIdentificacion> TipoIdentificaciones { get; set; }
    }
}
