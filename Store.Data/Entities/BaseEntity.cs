namespace Store.Data.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreateAt { get; set; }= DateTime.Now;
    }
}
