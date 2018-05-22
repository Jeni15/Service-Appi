using Base.Service.Services;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;

namespace SeedProject
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validar la contraseña del usuario
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                // Esto no cuenta los errores de inicio de sesión hacia el bloqueo de cuenta
                // Para habilitar los errores de contraseña para desencadenar el bloqueo, cambie a shouldLockout: true
                var result = signinManager.PasswordSignIn(email.Text,password.Text, remember.Checked, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        Response.Redirect("/Default");
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("/Account/Lockout");
                        break;
                    case SignInStatus.Failure:
                    default:
                        //FailureText.Text = "Intento de inicio de sesión no válido";
                        //ErrorMessage.Visible = true;                        
                        break;
                }
            }
        }
    }
}