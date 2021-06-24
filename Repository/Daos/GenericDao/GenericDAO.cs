using Microsoft.EntityFrameworkCore;
using Repository.DAOs.Collections;
using Repository.Exceptions;
using System;
using System.Collections.Generic;
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

        public virtual IPageList<E> GetPagedList(int pageSize, int pageIndex, Func<E, E> orderBy, Func<E, bool> predicate = null)
        {
            if (pageSize <= 0)
            {
                throw new PageSizeException(pageSize);
            }

            int totalPages = (int)Math.Ceiling((double)context.Set<E>().Count() / pageSize);
            if (pageIndex < 0 || pageIndex > totalPages)
            {
                throw new PageIndexException(pageIndex, totalPages);
            }

            IQueryable<E> query = context.Set<E>();
            if (predicate == null)
            {
                IList<E> items = context.Set<E>().Skip(pageSize * pageIndex).Take(pageSize).OrderBy(orderBy).ToList();

                return new PageList<E>()
                {
                    PageSize = pageSize,
                    PageIndex = pageIndex,
                    TotalPages = totalPages,
                    Items = items
                };
            }
            else
            {
                IList<E> items = context.Set<E>().Where(predicate).Skip(pageSize * pageIndex).Take(pageSize).OrderBy(orderBy).ToList();

                return new PageList<E>()
                {
                    PageSize = pageSize,
                    PageIndex = pageIndex,
                    TotalPages = totalPages,
                    Items = items
                };
            }
        }
    }
}