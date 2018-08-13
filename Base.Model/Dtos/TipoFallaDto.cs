
using Base.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Dtos
{
    public class TipoFallaDto
    {
        public long IdConsulta { get; set; }
        public string Comentario { get; set; }
        public List<TipoFalla> TipoFallas { get; set; }
    }
}
