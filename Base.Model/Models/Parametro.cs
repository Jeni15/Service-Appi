using System;
using System.Text;
using System.Collections.Generic;

namespace Base.Model.Models
{
    public class Parametro
    {
        public long IDParameter { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string AliasGAMS { get; set; }

        public long IdUOM { get; set; }

        public long Cantidad_Dimensiones { get; set; }

        public string Dimension { get; set; }

        public string Entrada_Manual { get; set; }

        public string Resultado { get; set; }

        public DateTime Fecha_Creacion { get; set; }

        public string Usuario_Creacion { get; set; }

        public DateTime Fecha_UltMod { get; set; }

        public string Usuario_UltMod { get; set; }

        public string Activa { get; set; }

        public long IdVersion { get; set; }
    }
}