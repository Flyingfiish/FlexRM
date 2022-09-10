using FlexRM.Core.Domain.Specifications;
using FlexRM.Core.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexRM.Core.Infrastructure.Repository
{
    public class EFRepository<T> : IRepository<T> where T : class, new()
    {
        protected ApplicationContext _context;
        protected DbSet<T> _entities;

        public EFRepository(ApplicationContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async virtual Task Delete(T entity)
        {
            //List<T> entity = await _entities.Where(spec.ToExpression()).ToListAsync();
            _entities.RemoveRange(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
        }

        public void Dispose()
        {
            _context.DisposeAsync();
        }

        public async virtual Task<T> ReadOne(ISpecification<T> spec)
        {
            IQueryable<T> query = _context.Set<T>();
            if (spec.Includes != null)
            {
                query = spec.Includes(query);
            }

            return await query
                .AsNoTracking()
                .FirstOrDefaultAsync(spec.ToExpression());
        }

        public async Task<List<T>> ReadMany(ISpecification<T> spec)
        {
            IQueryable<T> query = _context.Set<T>();
            if (spec.Includes != null)
            {
                query = spec.Includes(query);
            }
            return await query
                .AsNoTracking()
                .Where(spec.ToExpression()).ToListAsync();
        }

        public async Task<T> Update(T entity) //ISpecification<T> spec, Action<T> func)
        {
            //T entity = await _entities.FirstOrDefaultAsync(spec.ToExpression());
            //func(entity);
            _entities.Update(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
    }
}
