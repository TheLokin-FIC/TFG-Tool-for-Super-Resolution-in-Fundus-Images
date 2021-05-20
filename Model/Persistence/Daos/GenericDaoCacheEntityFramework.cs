using Microsoft.EntityFrameworkCore;
using Model.Persistence.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using Model.Persistence.Exceptions;

namespace Model.Persistence.Daos
{
    public class GenericDaoCacheEntityFramework<E, PK> : IGenericDao<E, PK> where E : class
    {
        protected readonly DbContext context;
        protected readonly Type entityClass;

        public GenericDaoCacheEntityFramework(DbContext context)
        {
            this.context = context;
            entityClass = typeof(E);
        }

        public void Create(E entity)
        {
            context.Set<E>().Add(entity);
            context.SaveChanges();

            CacheManager.ExpireTag(entityClass.Name);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public E Find(PK id)
        {
            string cacheKey = entityClass.Name + "Find=" + id;

            if (CacheManager.Exists(cacheKey))
            {
                return (E)CacheManager.Get<E>(cacheKey);
            }
            else
            {
                E result = context.Set<E>().Find(id);

                if (result == null)
                {
                    throw new InstanceNotFoundException(id, entityClass.FullName);
                }

                CacheManager.Set(cacheKey, result, entityClass.Name);

                return result;
            }
        }

        public void Update(E entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            CacheManager.ExpireTag(entityClass.Name);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void Remove(PK id)
        {
            E objectToRemove = default(E);

            objectToRemove = Find(id);

            context.Set<E>().Remove(objectToRemove);
            context.SaveChanges();

            CacheManager.ExpireTag(entityClass.Name);
        }

        public bool Exists(PK id)
        {
            string cacheKey = entityClass.Name + "Find=" + id;

            if (CacheManager.Exists(cacheKey))
            {
                return true;
            }
            else
            {
                E result = context.Set<E>().Find(id);

                if (result == null)
                {
                    return false;
                }

                CacheManager.Set(cacheKey, result, entityClass.Name);

                return true;
            }
        }

        public IList<E> GetAllElements()
        {
            string cacheKey = entityClass.Name + "GetAllElements";

            if (CacheManager.Exists(cacheKey))
            {
                return CacheManager.Get<List<E>>(cacheKey);
            }
            else
            {
                List<E> result = context.Set<E>().ToList();

                CacheManager.Set(cacheKey, result, entityClass.Name);

                return result;
            }
        }

        protected T FromCache<T>(string cacheKey, Func<T> adquire, params string[] tags)
        {
            if (CacheManager.Exists(cacheKey))
            {
                return CacheManager.Get<T>(cacheKey);
            }
            else
            {
                T result = adquire();

                CacheManager.Set(cacheKey, result, tags);

                return result;
            }
        }
    }
}