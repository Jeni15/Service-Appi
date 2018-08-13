using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Models
{
    public class TipoFalla
    {
        public long Id { get; set; }

        public string Nombre { get; set; }

        public string Comentario { get; set; }

        /*-------------------------------------------
         * PROPIEDADES DE CONSULTA
         * ------------------------------------------*/
        public long IdConsulta { get; set; }
       
    
    }
}
