using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Models
{
    public class MultisetFormViewModel
    {
        public List<Multiset> Multisets { get; set; }

        public Multiset Multiset { get; set; }

        public List<Modelo> Modelos { get; set; }

        public List<Version> Versiones { get; set; }
    }
}
