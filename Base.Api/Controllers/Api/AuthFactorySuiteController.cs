using Base.Api.Utils;
using Base.Model.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml;

namespace Base.Api.Controllers.Api
{
    public class AuthFactorySuiteController : ApiController
    {

        public AuthFactorySuiteController()
        {
        }

        //POST /api/authfactorysuite/token
        [Route("api/auth/token")]
        [HttpPost]        
        public async Task<IHttpActionResult> Token()
        {
            try
            {
                NetworkCredential credentials = GetCredentials();

                string token = await Authenticate(credentials);

                var userToken = new UserToken { IdToken = token };

                return Ok(userToken);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.Unauthorized, ex.Message);
            }
        }

        //POST /api/authfactorysuite/ejecutar
        [Route("api/auth/ejecutar")]
        [HttpPost]
        public async Task<IHttpActionResult> Ejecutar()
        {
            try
            {
                string xml = GetMessage();

                string response = await Execute(xml);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        private NetworkCredential GetCredentials()
        {
            string authHeader = HttpContext.Current.Request.Headers["Authorization"];

            if (authHeader == null || !authHeader.StartsWith("Basic"))
                throw new Exception("Authorization header esta vacía o no es basic.");

            string usernamePassword = Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(authHeader.Substring("Basic ".Length).Trim()));

            int seperatorIndex = usernamePassword.IndexOf(':');

            return new NetworkCredential(usernamePassword.Substring(0, seperatorIndex), usernamePassword.Substring(seperatorIndex + 1));

        }

        private async Task<string> Authenticate(NetworkCredential credentials)
        {
            string result = "";

            HttpContent content = new StringContent(Convert.ToBase64String(Encoding.ASCII.GetBytes($"USUARIO={credentials.UserName}&CLAVE={credentials.Password}&ACCION=login")));

            using (var Client = new HttpClient())
            {
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                try
                {
                    var Response = await Client.PostAsync(ServiceUrl.FactorySuiteProxy, content);

                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        var ResultWebAPI = await Response.Content.ReadAsStringAsync();

                        XmlDocument document = new XmlDocument();

                        document.LoadXml(ResultWebAPI);

                        result = document.SelectSingleNode("/ROOT/XML/RETORNO/TOKEN").InnerText;

                        if (string.IsNullOrEmpty(result)) throw new Exception(document.SelectSingleNode("/ROOT/XML/RETORNO/MENSAJE").InnerText);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return result;
        }

        private string GetToken()
        {
            string authHeader = HttpContext.Current.Request.Headers["Authorization"];

            if (authHeader == null )
                throw new Exception("Authorization header esta vacía o no es basic.");

            return  Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(authHeader.Trim()));

        }

        private string GetMessage()
        {
            string xml = HttpContext.Current.Request.Form["xml"];

            if (xml == null)
                throw new Exception("Mensaje esta vacío.");

            return Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(xml.Trim()));

        }

        private async Task<string> Execute(string xml)
        {
            string result = "";

            HttpContent content = new StringContent(Convert.ToBase64String(Encoding.ASCII.GetBytes($"XML={xml}&ACCION=ejecutar")));

            using (var Client = new HttpClient())
            {
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                try
                {
                    var Response = await Client.PostAsync(ServiceUrl.FactorySuiteProxy, content);

                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        var ResultWebAPI = await Response.Content.ReadAsStringAsync();

                        //XmlDocument document = new XmlDocument();

                        //document.LoadXml(ResultWebAPI);

                        //result = document.SelectSingleNode("/ROOT/XML/RETORNO/TOKEN").InnerText;

                        result = ResultWebAPI;

                        //if (string.IsNullOrEmpty(result))
                        //    throw new Exception(document.SelectSingleNode("/ROOT/XML/RETORNO/MENSAJE").InnerText);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return result;
        }

    }
  
}
