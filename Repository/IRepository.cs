namespace E_Commerce.Repository
{
    public interface IRepository<T,T2>
    {
        public List<T> GetAll();
        public List<T> GetNewstItems(int numberOfItems);
        public List<T2> GetAllًWithNameAndIdOnly();
        public T GetById(int id,bool getProducts);
        public bool Add(T obj);
        public bool Update(T obj);
        public bool DeleteById(int id);

    }
}
