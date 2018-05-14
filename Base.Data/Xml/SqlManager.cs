using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Base.Data.Xml
{

    /// <summary>
    /// Clase estática que gestiona las consultas SQL, plasmadas en el archivo Queries.xml
    /// </summary>
    public static class SqlManager
    {
        /// <summary>
        /// Diccionario estático privado, que va a contener las consultas plasmadas en el archivo Queries.xml
        /// </summary>
        private static StringDictionary Queries;
        /// <summary>
        /// Método estático público que carga el archivo Queries XML en el diccionario y es llamado desde el archivo globas.asax de la aplicación
        /// </summary>
        public static void Create()
        {
            LoadSqlFile();
        }
        /// <summary>
        /// Método estático público que obtiene el archivo Queries.xml y lo carga en el diccionario
        /// </summary>
        public static void LoadSqlFile()
        {
            XmlDocument xml = new XmlDocument();
            Assembly asm = Assembly.GetExecutingAssembly();
            StreamReader _textStreamReader;
            _textStreamReader = new StreamReader(asm.GetManifestResourceStream("Base.Data.Xml.Queries.xml"));

            xml.Load(_textStreamReader);

            XmlNodeList resources = xml.SelectNodes("top/queries/SQL");
            Queries = new StringDictionary();
            foreach (XmlNode node in resources)
            {
                Queries.Add(node.Attributes["id"].Value, node.FirstChild.InnerText);
            }
        }
        /// <summary>
        /// Método estático público que obtiene la sentencia SQL, dado el ID o nombre de la consulta que está plasmada en el archivo Queries.xml
        /// </summary>
        /// <param name="strIdSQL"></param>
        /// <returns></returns>
        public static string GetSQL(string strIdSQL)
        {
            return Queries[strIdSQL].Trim();
        }
    }
}
