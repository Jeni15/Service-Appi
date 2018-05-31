using Base.Model.Models;
using Base.Service;
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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Register_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = username.Text, Email = email.Text, FirstName = firstname.Text, LastName = lastname.Text };
            IdentityResult result = manager.Create(user, password.Text);
            if (result.Succeeded)
            {
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                Response.Redirect("/Default");
            }
            else
            {
                //ErrorMessage.Text = result.Errors.FirstOrDefault();
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "showMessage();", false);

            }
        }
    }
}