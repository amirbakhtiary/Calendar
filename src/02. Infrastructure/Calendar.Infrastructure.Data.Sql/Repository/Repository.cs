using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Infrastructure.Data.Sql.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly CalendarContext _calendarContext;

        public Repository(CalendarContext calendarContext)
        {
            _calendarContext = calendarContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _calendarContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");

            await _calendarContext.AddAsync(entity);

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(Update)} entity must not be null");

            _calendarContext.Update(entity);

            return entity;
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(Remove)} entity must not be null");

            _calendarContext.Remove(entity);
        }
    }
}
