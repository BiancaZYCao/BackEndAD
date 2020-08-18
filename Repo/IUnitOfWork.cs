using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace BackEndAD.Repo
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        //Gets the db context; return the instance of TContext
        TContext DbContext { get; }

        // remove ensure-auto-history
        Task<int> SaveChangesAsync();
        int SaveChanges();
        IDbContextTransaction BeginTransaction();
        IGenericRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;
        int ExecuteSqlCommand(string sql, params object[] parameters);
        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;
    }
}
