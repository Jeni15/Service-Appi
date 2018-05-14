using System;
using System.Text;
using System.Collections.Generic;

namespace Base.Model.Models
{
    public class Parametro
    {
        public int IdParametro { get; set; }

        public int? IdModeloCaso { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Alias { get; set; }

        public bool Activo { get; set; }
        
    }
}