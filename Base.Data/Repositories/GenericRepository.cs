using Base.Data.Extensions;
using Base.Data.Infrastructure;
using Base.Data.Xml;
using Base.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.Repositories
{

    public class GenericRepository<T> : AdoNetRepository<T>, IGenericRepository<T> where T : class
    {
        public GenericRepository(IAdoNetDbFactory dbFactory, IUnitOfWork uow) : base(dbFactory,uow)
        {
           
        }

        public void AddGeneric(T entity)
        {

            using (var command = (_uow?.CreateCommand() ?? DbContext.Connection.CreateCommand()))
            {

                var sql = "prueba";

                DbParameter[] parameters = CreateParemeters(entity, command, sql);

                foreach (var param in parameters) command.Parameters.Add(param);

                command.CommandText = sql;

                command.ExecuteNonQuery();

            }

        }

        public void DeleteGeneric(T entity)
        {
            using (var command = (_uow?.CreateCommand() ?? DbContext.Connection.CreateCommand()))
            {

                var sql = "chawa chawa chawaaaaa yehhhhh ...";

                DbParameter[] parameters = CreateParemeters(entity, command, sql);

                foreach (var param in parameters) command.Parameters.Add(param);

                command.CommandText = sql;

                command.ExecuteNonQuery();

            }

        }

        public void UpdateGeneric(T entity)
        {
            using (var command = (_uow?.CreateCommand() ?? DbContext.Connection.CreateCommand()))
            {

                var sql = "Michelle Sofia";

                DbParameter[] parameters = CreateParemeters(entity, command, sql);

                foreach (var param in parameters) command.Parameters.Add(param);

                command.CommandText = sql;

                command.ExecuteNonQuery();

            }

        }

        public T GetByIdGeneric(int id)
        {
            using (var command = (_uow?.CreateCommand() ?? DbContext.Connection.CreateCommand()))
            {

                var sql = "";

                //DbParameter[] parameters = CreateParemeters(entity, command, sql);

                command.Parameters.Add(command.CreateParameter("@Id", id));

                command.CommandText = sql;

                return this.ToList(command).FirstOrDefault();
            }
        }

        public IEnumerable<T> GetAllGeneric()
        {
            using (var command = (_uow?.CreateCommand() ?? DbContext.Connection.CreateCommand()))
            {

                var sql = "";

                //DbParameter[] parameters = CreateParemeters(entity, command, sql);

                //foreach (var param in parameters) command.Parameters.Add(param);

                command.CommandText = sql;

                return this.ToList(command);

            }
        }

    }

    public interface IGenericRepository<T> : IRepository<T> where T : class
    {
        void AddGeneric(T entity);
        void DeleteGeneric(T entity);
        void UpdateGeneric(T entity);
        T GetByIdGeneric(int id);
        IEnumerable<T> GetAllGeneric();

    }
}
