using System;
using System.Text;
using System.Collections.Generic;

namespace Base.Model.Models
{
    public class Modelo
    {
        public long IdModelo { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime Fecha_Creacion { get; set; }

        public string Usuario_Creacion { get; set; }

        public DateTime Fecha_UltMod { get; set; }

        public string Usuario_UltMod { get; set; }

        public string Activa { get; set; }
    }
}