using System;
using System.Text;
using System.Collections.Generic;

namespace Base.Model.Models
{
    public class Menu
    {
        public int IdMenu { get; set; }

        public int? IdMenuPadre { get; set; }

        public string Nombre { get; set; }

        public string Url { get; set; }

        public string Icono { get; set; }
        
    }
}