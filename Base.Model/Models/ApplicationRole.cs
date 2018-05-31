using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Models
{
    public class ApplicationRole : IdentityRole
    {
        //public bool EsVisible { get; set; }

        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }

    }
}
