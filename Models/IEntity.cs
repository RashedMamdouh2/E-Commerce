namespace E_Commerce.Models
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
