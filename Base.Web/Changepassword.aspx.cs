using Base.Model.Models;
using Base.Service.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeedProject
{
    public partial class Changepassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CambiarClave_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindByName(username.Text);
            
            if (user == null)
            {
                return;
            }

            if (!manager.CheckPassword(user, currentpassword.Text))
            {
                return;
            }

            IdentityResult result = manager.ChangePassword(user.Id, currentpassword.Text,newpassword.Text);
            if (result.Succeeded)
            {
                //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else
            {
                //ErrorMessage.Text = result.Errors.FirstOrDefault();
                return;
            }
        }
    }
}