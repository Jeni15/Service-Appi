using Base.Data.Infrastructure;
using Base.Data.Repositories;
using Base.Model.Models;
using Base.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Service.Services
{
   
    public static class ExceptionService
    {
        public static string ConvertirError(Exception ex)
        {
            string msgError = "";

            if (ex.GetType() == typeof(SqlException))
            {
                SqlException sqlError = (SqlException)ex;

                if (sqlError.Number == 109 || sqlError.Number == 110)
                {
                    msgError = "Hay un problema en el comando de inserción utilizado. Contacte al administrador.";
                }
                else if (sqlError.Number == 2601)
                {
                    msgError = "Ya existe un registro con la información suministrada. Verifique los datos ingresados.";
                }
                else if (sqlError.Number == 515)
                {
                    msgError = "No esta llegando un valor requerido para el comando de base de datos. Contacte al administrador.";
                }
                else if (sqlError.Number == 156)
                {
                    msgError = "Error de sintaxis en el comando utilizado. Contacte al administrador.";
                }
                else if (sqlError.Number == 547)
                {
                    msgError = "Error al eliminar el registro, hay informacíón enlazada.";
                }
                else if (sqlError.Number == 18456)
                {
                    msgError = "Se pretento un problema al acceder a la base de datos. Contacte al administrador.";
                }
                else
                {
                    msgError = "Se ha presentado un problema en base de datos. Contacte al administrador.";
                }
            }
            else
            {
                msgError = ex.Message;
            }

            return msgError;
        }
    }
}
