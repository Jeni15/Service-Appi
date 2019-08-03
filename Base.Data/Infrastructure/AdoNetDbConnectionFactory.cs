using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.Infrastructure
{
    public class AdoNetDbConnectionFactory : IAdoNetDbConnectionFactory
    {

        private readonly DbProviderFactory _provider;
        private readonly string _connectionString;
        private readonly string _providerName;
        private DbConnection _connection;
        
        public AdoNetDbConnectionFactory()
        {
            var connectionName = "DefaultConnection";
            var conStr = ConfigurationManager.ConnectionStrings[connectionName];
            if (conStr == null) throw new ConfigurationErrorsException(string.Format("Falló al crear una cadena de conexión utilizando el nombre de conexion '{0}' en app/web.config.", connectionName));

            _providerName = conStr.ProviderName;
            _provider = DbProviderFactories.GetFactory(conStr.ProviderName);
            _connectionString = conStr.ConnectionString;
            _connection = (DbConnection)CreateConnection();

        }       
        
        public IDbConnection Create()
        {
            return _connection ?? (_connection = (DbConnection)CreateConnection());
        }

        private IDbConnection CreateConnection()
        {
            var connection = _provider.CreateConnection();
            if (connection == null) throw new ConfigurationErrorsException(string.Format("Falló al crear una conexión."));

            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }

    }
}
