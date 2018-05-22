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
        }

        #endregion  

        #region Methods

        public void Add(T entity)
        {

            using (var command = (_uow?.CreateCommand() ?? DbContext.Connection.CreateCommand()))
            {

                var sql = SqlManager.GetSQL($"{typeof(T).Name}_Insert");

                DbParameter[] parameters = CreateParemeters(entity, command, sql);

                foreach (var param in parameters) command.Parameters.Add(param);

                command.CommandText = sql;

                command.ExecuteNonQuery();

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

                command.Parameters.Add(command.CreateParameter("@Id", id));

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
                    property.SetValue(objT, record[property.Name]);
            }
            return objT;
        }

        protected DbParameter[] CreateParemeters(T entity, IDbCommand command, string sql)
        {
            var pattern = "@([a-zA-Z_]+)";

            DbParameter[] parameters = new DbParameter[0];

            MatchCollection matches = Regex.Matches(sql, pattern);

            foreach (Match param in matches)
            {
                foreach (var property in typeof(T).GetProperties())
                {
                    if ($"@{property.Name}".ToUpper().Equals(param.Value.ToUpper()))
                    {
                        object value = property.GetValue(entity, null);

                        if (property.PropertyType == typeof(DateTime))
                            if ( !((DateTime?)value).HasValue || (DateTime?)value == default(DateTime)) value = DBNull.Value;

                        Array.Resize(ref parameters, parameters.Length + 1);
                        parameters[parameters.Length - 1] = (DbParameter)command.CreateParameter(param.Value, value);
                        break;
                    }
                }
            }
            return parameters;
        }

        #endregion

    }
}
