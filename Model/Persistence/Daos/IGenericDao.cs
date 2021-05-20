using Model.Persistence.Exceptions;
using System.Collections.Generic;

namespace Model.Persistence.Daos
{
    public interface IGenericDao<E, PK>

    {
        void Create(E entity);

        /// <exception cref="InstanceNotFoundException"/>
        E Find(PK id);

        bool Exists(PK id);

        void Update(E entity);

        /// <exception cref="InstanceNotFoundException"/>
        void Remove(PK id);

        IList<E> GetAllElements();
    }
}