using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Repository.DAOs.GenericDAO.CacheDAO
{
    public class CacheDAO<E> : GenericDAO<E> where E : class
    {
        public CacheDAO(DbContext context) : base(context)
        {
        }

        public override void Insert(E entity)
        {
            base.Insert(entity);
            CacheManager.ExpireTag(typeof(E));
        }

        public override bool Exists(params object[] keyValues)
        {
            return FromCache($"{typeof(E).Name}Exists={string.Join(", ", keyValues)}", () =>
            {
                return base.Exists(keyValues);
            }, typeof(E));
        }

        public override E Find(params object[] keyValues)
        {
            return FromCache($"{typeof(E).Name}Find={string.Join(",", keyValues)}", () =>
            {
                return base.Find(keyValues);
            }, typeof(E));
        }

        public override void Update(E entity)
        {
            base.Update(entity);
            CacheManager.ExpireTag(typeof(E));
        }

        public override void Delete(params object[] keyValues)
        {
            base.Delete(keyValues);
            CacheManager.ExpireTag(typeof(E));
        }

        public override IQueryable<E> GetAll()
        {
            return FromCache($"{typeof(E).Name}GetAll", () =>
            {
                return base.GetAll();
            }, typeof(E));
        }

        protected T FromCache<T>(string cacheKey, Func<T> adquire, params Type[] tags)
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