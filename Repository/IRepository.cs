namespace E_Commerce.Repository
{
    public interface IRepository<T>
    {
        public List<T> GetAll();
        public T GetById(int id);
    }
}
