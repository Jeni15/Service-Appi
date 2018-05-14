using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int TipoIdentificacionId { get; set; }

        public TipoIdentificacion TipoIdentificacion { get; set; }

    }
}
