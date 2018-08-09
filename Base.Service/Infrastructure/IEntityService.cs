using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Service.Infrastructure
{
    public interface IEntityService<T> where T : class
    {
        long Create(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        //IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);
        T GetById(int id);
        void Update(T entity);
        IEnumerable<T> Execute(string statement, T entity);
    }
}
