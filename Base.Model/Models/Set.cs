using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Base.Model.Models
{
    public class Set
    {

        public long IDSet { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string AliasGAMS { get; set; }

        public long IDSet_Padre { get; set; }

        public DateTime Fecha_Creacion { get; set; }

        public string Usuario_Creacion { get; set; }

        public DateTime Fecha_UltMod { get; set; }

        public string Usuario_UltMod { get; set; }

        public string Activa { get; set; }

        public long IdVersion { get; set; }
    }
}