using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TasteRazor.DataAccess.Repository.Contracts;

namespace TasteRazor.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal readonly DbSet<T> EntitySet;

        public Repository(DbContext context)
        {
            Context = context;
            EntitySet = context.Set<T>();
        }

        private IQueryable<T> FormatQuery(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null,
            bool enableTracking = false)
        {
            IQueryable<T> query = EntitySet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                var properties = includeProperties.Split(
                    new char[] {','},
                    StringSplitOptions.RemoveEmptyEntries);

                query = properties.Aggregate(
                    query,
                    (current, property) => current.Include(property));
            }

            if (!enableTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }

        public T Get(int id)
        {
            return EntitySet.Find(id);
        }

        public async Task<T> GetAsync(int id)
        {
            return await EntitySet.FindAsync(id);
        }

        public IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null)
        {
            var query = FormatQuery(filter, includeProperties);

            return orderBy == null ? query.ToList() : orderBy(query).ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null)
        {
            var query = FormatQuery(filter, includeProperties);

            return orderBy == null
                ? await query.ToListAsync()
                : await orderBy(query).ToListAsync();
        }

        public T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null,
            bool enableTracking = false)
        {
            var query = FormatQuery(filter, includeProperties, enableTracking);

            return query.FirstOrDefault();
        }

        public async Task<T> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null,
            bool enableTracking = false)
        {
            var query = FormatQuery(filter, includeProperties, enableTracking);

            return await query.FirstOrDefaultAsync();
        }

        public void Add(T entity)
        {
            EntitySet.Add(entity);
        }

        public void Remove(int id)
        {
            Remove(EntitySet.Find(id));
        }

        public void Remove(T entity)
        {
            EntitySet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Remove(entity);
            }
        }
    }
}
