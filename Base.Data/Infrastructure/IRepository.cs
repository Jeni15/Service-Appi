using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        long Add(T entity);

        // Marks an entity as modified
        void Update(T entity);

        // Marks an entity to be removed
        void Delete(T entity);

        // Get an entity by int id
        T GetById(int id);

        // Gets all entities of type T
        IEnumerable<T> GetAll();


        IEnumerable<T> Execute(string statement, T entity);

    }
}
