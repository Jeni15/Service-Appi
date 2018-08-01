using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Models
{
    public class SetFormViewModel
    {
        public List<Set> Sets { get; set; }

        public Set Set { get; set; }

        public List<Version> Versiones { get; set; }
    }
}
