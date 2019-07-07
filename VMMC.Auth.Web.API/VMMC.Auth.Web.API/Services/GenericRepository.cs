using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMMC.Auth.Web.API.Data;
using VMMC.Auth.Web.API.Db;

namespace VMMC.Auth.Web.API.Services
{
    /// <summary>
    /// GenericRepository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly ApplicationDbContext _DbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dBContext"></param>
        public GenericRepository(ApplicationDbContext dBContext)
        {
            this._DbContext = dBContext;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> Create(TEntity entity)
        {
            await _DbContext.Set<TEntity>().AddAsync(entity);
            return await _DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(int id)
        {
            var entity = await _DbContext.Set<TEntity>().FindAsync(id);
            _DbContext.Set<TEntity>().Remove(entity);
           return await _DbContext.SaveChangesAsync();

        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            return _DbContext.Set<TEntity>().AsNoTracking();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> Update(TEntity entity)
        {
            _DbContext.Set<TEntity>().Update(entity);
            return await _DbContext.SaveChangesAsync();
        }
    }
}
