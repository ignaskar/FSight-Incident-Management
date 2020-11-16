using System;
using System.Threading.Tasks;
using FSight.Core.Entities;

namespace FSight.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> Complete();
    }
}