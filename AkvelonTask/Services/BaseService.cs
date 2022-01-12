using AkvelonTask.Data;
using AkvelonTask.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AkvelonTask.Services
{
    public abstract class BaseService<T> : IBaseService<T> where T : BaseModel
    {
        protected readonly AppDbContext _dbContext;
        public BaseService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().Where(z => !z.IsDeleted).ToListAsync();
        }
        public virtual async Task<T> Get(int id)
        {
            return await EntityExist(id);
        }
        public async Task<int> Add(T model)
        {
            await _dbContext.Set<T>().AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }

        public async Task Delete(int id)
        {
            var ent = await EntityExist(id);
            ent.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }
        public  async Task Update(T model)
        {
            await EntityExist(model.Id);
            _dbContext.Set<T>().Update(model);
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Throws an Exception if entity does not exist;
        /// Returns it otherwise;
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Generic T</returns>
        public virtual async Task<T> EntityExist(int id)
        {
            var ent = await _dbContext.Set<T>().FindAsync(id);
            if (ent == null)
            {
                ThrowDoesNotExistException();
            }
            return ent;
        }
        protected void ThrowDoesNotExistException()
        {
            string s = typeof(T).Name;
            var exception = new Exception($"{s} does not exist.");
            exception.Data["StatusCode"] = 404;
            throw exception;
        }
    }
}
