using System;
using System.Collections;
using System.Threading.Tasks;
using FSight.Core.Entities;
using FSight.Core.Interfaces;

namespace FSight.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FSightContext _context;
        private Hashtable _repositories;

        public UnitOfWork(FSightContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            _repositories ??= new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance =
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                
                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>) _repositories[type];
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
    }
}