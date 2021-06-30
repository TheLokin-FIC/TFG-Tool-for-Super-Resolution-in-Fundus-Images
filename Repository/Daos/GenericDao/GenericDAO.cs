using Microsoft.EntityFrameworkCore;
using Repository.Exceptions;
using System;
using System.Linq;

namespace Repository.DAOs.GenericDAO
{
    public class GenericDAO<E> : IDAO<E> where E : class
    {
        protected readonly DbContext context;

        /// <exception cref="ArgumentNullException"/>
        public GenericDAO(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual void Insert(E entity)
        {
            context.Set<E>().Add(entity);
            context.SaveChanges();
        }

        public virtual bool Exists(params object[] keyValues)
        {
            return context.Set<E>().Find(keyValues) == null;
        }

        public virtual E Find(params object[] keyValues)
        {
            return context.Set<E>().Find(keyValues) ?? throw new InstanceNotFoundException(typeof(E), keyValues);
        }

        public virtual void Update(E entity)
        {
            context.Set<E>().Update(entity);
            context.SaveChanges();
        }

        public virtual void Delete(params object[] keyValues)
        {
            context.Set<E>().Remove(Find(keyValues));
            context.SaveChanges();
        }

        public virtual IQueryable<E> GetAll()
        {
            return context.Set<E>();
        }
    }
}