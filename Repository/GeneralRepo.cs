using E_Commerce.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Commerce.Repository
{
    public class GeneralRepo<T,IdType> : IGeneralRepo<T,IdType> where T : class,IEntity<IdType>
    {
        private readonly CommerceDbContext context;

        public GeneralRepo(CommerceDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddAsync(T obj)
        {
            try
            {
                await context.AddAsync(obj);

                
            }
            catch (Exception ex) {

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(ex.InnerException.Message);
                return false;
            
            }
            return true;
        }

        public async Task<bool> DeleteByIdAsync(IdType id)
        {
            try
            {
                var obj =await this.GetByIdAsync(id);
                context.Set<T>().Remove(obj);
            await SaveAsync();
            }
            catch (Exception ex)
            {

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(ex.InnerException.Message);
                return false;

            }
            return true;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(IdType id)
        {
            T res = null;
            try
            {
                res= await context.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(ex.InnerException.Message);
               

            }
            
            return res;
        }

        public async Task<List<T>> GetListByIdAsync(List<IdType> ids)
        {
            List<T> res = null;
            try
            {
                res = await context.Set<T>().Where(x=>ids.Contains(x.Id)).ToListAsync();
            }
            catch (Exception ex)
            {

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(ex.InnerException.Message);


            }

            return res;
        }
        public async Task<bool> UpdateAsync(T obj)
        {
            
            try
            {
                context.Update(obj);

            }
            catch (Exception ex)
            {

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(ex.InnerException.Message);

                return false;
            }

            return true;
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex) {


                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(ex.InnerException.Message);

                return false;
            }
            return true;
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> filter, string[] includes)
        {
             Expression<Func<T,bool>> ex=filter;
            
            var query =  context.Set<T>().AsQueryable();
            foreach (var include in includes)
            {
                query= query.Include(include);
            }
            return await query.FirstOrDefaultAsync(ex);
        }
        public  IEnumerable<T> FindAll(Expression<Func<T, bool>> filter, string[] includes,int take =-1,int skip=-1)
        {
           
             Expression<Func<T,bool>> ex=filter;
            
            IQueryable<T> query =  context.Set<T>();
            foreach (var include in includes)
            {
               query= query.Include(include);
            }
            var res= query.Where(ex);
            if(take >= 0)
            {
               res = res.Take(take);
            }
            if (skip >= 0) { 
                res = res.Skip(skip);
            }
            return res.AsEnumerable();
        }
    }
}
