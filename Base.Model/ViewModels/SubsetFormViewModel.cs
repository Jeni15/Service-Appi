using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Models
{
    public class SubsetFormViewModel
    {
        public List<Subset> Subsets { get; set; }

        public Subset Subset { get; set; }

        public List<Modelo> Modelos { get; set; }

        public List<Version> Versiones { get; set; }
    }
}
