using System;
using System.Data;

namespace Base.Data.Infrastructure
{
    public class AdoNetDbContext
    {
        private readonly IAdoNetDbConnectionFactory _connectionFactory;
        private  IDbConnection _connection ;

        public AdoNetDbContext()
        {
            _connectionFactory = new AdoNetDbConnectionFactory();
        }

        public IDbConnection Connection
        {
            get { return _connection ?? (_connection = _connectionFactory.Create()); }
        }


        public void Dispose()
        {
            _connection?.Dispose();
            _connection = null;
        }
        
    }
}
