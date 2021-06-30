using Repository.Exceptions;
using System.Linq;

namespace Repository.DAOs
{
    public interface IDAO<E> where E : class
    {
        void Insert(E entity);

        bool Exists(params object[] keyValues);

        /// <exception cref="InstanceNotFoundException"/>
        E Find(params object[] keyValues);

        void Update(E entity);

        /// <exception cref="InstanceNotFoundException"/>
        void Delete(params object[] keyValues);

        IQueryable<E> GetAll();
    }
}