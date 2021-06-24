using Repository.DAOs.Collections;
using Repository.Exceptions;
using System;
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

        /// <exception cref="PageSizeException"/>
        /// <exception cref="PageIndexException"/>
        IPageList<E> GetPagedList(int pageSize, int pageIndex, Func<E, E> orderBy, Func<E, bool> predicate = null);
    }
}