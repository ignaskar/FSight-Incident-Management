using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSight.Core.Entities;
using FSight.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FSight.Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        private readonly FSightContext _context;

        public GenericRepository(FSightContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<T> GetEntityById(int id)
        {
            return await _context.Set<T>()
                .FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>()
                .ToListAsync();
        }

        public async Task<T> GetEntityWithSpecification(ISpecification<T> spec)
        {
            return await ApplySpecification(spec)
                .FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec)
                .ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}