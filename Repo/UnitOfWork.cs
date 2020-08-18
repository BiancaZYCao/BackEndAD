using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using BackEndAD.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace BackEndAD.Repo
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        protected readonly TContext _context;
        private bool disposed = false;
        private Dictionary<Type, object> repositories;

        // Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.

        public UnitOfWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        //Gets the db context
        public TContext DbContext => _context;

        public IDbContextTransaction BeginTransaction()
        {
            return DbContext.Database.BeginTransaction();
        }

        /// <summary>
        /// Gets the specified repository for the <typeparamref name="TEntity"/>.
        /// </summary>
        public IGenericRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            Type type = typeof(IGenericRepository<TEntity>);
            if (!repositories.TryGetValue(type, out object repo))
            {
                IGenericRepository<TEntity> newRepo = new GenericRepository<TEntity>(DbContext);
                repositories.Add(type, newRepo);
                return newRepo;
            }
            return (IGenericRepository<TEntity>)repo;
        }

        // Executes the specified raw SQL command.
        public int ExecuteSqlCommand(string sql, params object[] parameters) => DbContext.Database.ExecuteSqlRaw(sql, parameters);

        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class => DbContext.Set<TEntity>().FromSqlRaw(sql, parameters);

        
        public async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }
        public int SaveChanges()
        {
            return DbContext.SaveChanges();
            
        }


        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // clear repositories
                    if (repositories != null)
                    {
                        repositories.Clear();
                    }

                    // dispose the db context.
                    DbContext.Dispose();
                }
            }

            disposed = true;
        }

        public void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback)
        {
            DbContext.ChangeTracker.TrackGraph(rootEntity, callback);
        }
    }
}