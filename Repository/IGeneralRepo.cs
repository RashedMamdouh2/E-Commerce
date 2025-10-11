using E_Commerce.Models;
using System.Linq.Expressions;

namespace E_Commerce.Repository
{
    public interface IGeneralRepo<T,IdType> where T : IEntity<IdType> 
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByIdAsync(IdType id);
        public Task<List<T>> GetListByIdAsync(List<IdType> ids);
        public Task<bool> AddAsync(T obj);
        public Task<bool> UpdateAsync(T obj);
        public Task<bool> DeleteByIdAsync(IdType id);
        public  Task<T> FindAsync(Expression<Func<T,bool>>filter,string[] includes);
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> filter, string[] includes,int take =-1 ,int skip=-1);


        public Task<bool> SaveAsync();

    }
}
