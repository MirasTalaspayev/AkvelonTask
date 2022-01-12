using AkvelonTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkvelonTask.Services
{
    public interface IBaseService<T> where T : BaseModel
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Get(int id);
        public Task<int> Add(T model);
        public Task Update(T model);
        public Task<T> EntityExist(int id);
        public Task Delete(int id);
    }
}
