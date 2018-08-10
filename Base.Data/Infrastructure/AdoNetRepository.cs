using Base.Data.Extensions;
using Base.Data.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Base.Data.Infrastructure
{
    public class AdoNetRepository<T> : IRepository<T> where T : class
    {

        #region Fields and Properties

        private AdoNetDbContext _dbContext;

        protected AdoNetUnitOfWork _uow;

        public string Comodin { get; set; }

        protected IAdoNetDbFactory DbFactory
        {
            get;
            private set;
        }

        protected AdoNetDbContext DbContext
        {
            get { return _dbContext ?? (_dbContext = DbFactory.Init()); }
        }

        #endregion

        #region Constructors 

        public AdoNetRepository(IAdoNetDbFactory dbFactory, IUnitOfWork unitOfWork)
        {
            DbFactory = dbFactory;
            _uow = unitOfWork as AdoNetUnitOfWork;
            Comodin = ":";
        }

        #endregion  

        #region Methods

        public long Add(T entity)
        {

            using (var command = (_uow?.CreateCommand() ?? DbContext.Connection.CreateCommand()))
            {

                var sql = SqlManager.GetSQL($"{typeof(T).Name}_Insert");

                DbParameter[] parameters = CreateParemeters(entity, command, sql);

                foreach (var param in parameters) command.Parameters.Add(param);

                command.CommandText = sql;

                command.ExecuteNonQuery();

                var parameter = command.Parameters[":Id"];

                if (parameter == null)
                    return 0;

                var parameterType = parameter.GetType();


                var id = parameterType.GetProperty("Value").GetValue(parameter); //command.Parameters[":Id"].ToString();

                if (id != null)
                    return Convert.ToInt64(id);


                return 0;

            }

        }

        public void Delete(T entity)
        {
            using (var command = (_uow?.CreateCommand() ?? DbContext.Connection.CreateCommand()))
            {

                var sql = SqlManager.GetSQL($"{typeof(T).Name}_Delete");

                DbParameter[] parameters = CreateParemeters(entity, command, sql);

                foreach (var param in parameters) command.Parameters.Add(param);

                command.CommandText = sql;

                command.ExecuteNonQuery();

            }

        }

        public void Update(T entity)
        {
            using (var command = (_uow?.CreateCommand() ?? DbContext.Connection.CreateCommand()))
            {

                var sql = SqlManager.GetSQL($"{typeof(T).Name}_Update");

                DbParameter[] parameters = CreateParemeters(entity, command, sql);

                foreach (var param in parameters) command.Parameters.Add(param);

                command.CommandText = sql;

                command.ExecuteNonQuery();

            }

        }

        public T GetById(int id)
        {
            using (var command = (_uow?.CreateCommand() ?? DbContext.Connection.CreateCommand()))
            {

                var sql = SqlManager.GetSQL($"{typeof(T).Name}_Get");

                //DbParameter[] parameters = CreateParemeters(entity, command, sql);

                command.Parameters.Add(command.CreateParameter($"{Comodin}Id", id));

                command.CommandText = sql;

                return this.ToList(command).FirstOrDefault();
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var command = (_uow?.CreateCommand() ?? DbContext.Connection.CreateCommand()))
            {

                var sql = SqlManager.GetSQL($"{typeof(T).Name}_GetAll");

                //DbParameter[] parameters = CreateParemeters(entity, command, sql);

                //foreach (var param in parameters) command.Parameters.Add(param);

                command.CommandText = sql;

                return this.ToList(command);

            }
        }

        public IEnumerable<T> Execute(string statement, T entity)
        {
            using (var command = (_uow?.CreateCommand() ?? DbContext.Connection.CreateCommand()))
            {

                var sql = SqlManager.GetSQL($"{typeof(T).Name}_{statement}");

                DbParameter[] parameters = CreateParemeters(entity, command, sql);

                foreach (var param in parameters) command.Parameters.Add(param);

                command.CommandText = sql;

                return this.ToList(command);

            }
        }

        protected IEnumerable<T> ToList(IDbCommand command)
        {
            List<T> items = new List<T>();
            using (var record = command.ExecuteReader())
            {
                while (record.Read())
                {
                    items.Add(Map<T>(record));
                }
                return items;
            }
        }

        protected T Map<T>(IDataRecord record)
        {
            var objT = Activator.CreateInstance<T>();
            foreach (var property in typeof(T).GetProperties())
            {
                if (record.HasColumn(property.Name) && !record.IsDBNull(record.GetOrdinal(property.Name)))
                    if (property.PropertyType != typeof(char))
                    {
                        if (property.PropertyType == typeof(int) && record[property.Name].GetType() == typeof(decimal))
                            property.SetValue(objT, Convert.ToInt32(record[property.Name].ToString()));
                        else
                            property.SetValue(objT, record[property.Name]);
                    }
                    else
                        property.SetValue(objT, Convert.ToChar(record[property.Name]));
            }
            return objT;
        }

        protected DbParameter[] CreateParemeters(T entity, IDbCommand command, string sql)
        {
            var pattern = $"{Comodin}([a-zA-Z_]+)";

            List<string> listParameters = new List<string>();

            DbParameter[] parameters = new DbParameter[0];

            MatchCollection matches = Regex.Matches(sql, pattern);

            foreach (Match param in matches)
            {
                var index = listParameters.IndexOf(param.Value);

                if (index == -1)
                {
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if ($"{Comodin}{property.Name}".ToUpper().Equals(param.Value.ToUpper()))
                        {
                            object value = property.GetValue(entity, null);

                            if (property.PropertyType == typeof(DateTime))
                                if (!((DateTime?)value).HasValue || (DateTime?)value == default(DateTime)) value = null;

                            Array.Resize(ref parameters, parameters.Length + 1);
                            parameters[parameters.Length - 1] = (DbParameter)command.CreateParameter(param.Value, value ?? DBNull.Value);
                            break;
                        }
                    }
                }
                listParameters.Add(param.Value);
            }
            return parameters;
        }

        #endregion

    }
}
