namespace Cracker.Admin.Models
{
    public class RefAsync<T>
    {
        public T? Value { get; set; }

        public RefAsync()
        {
            Value = default;
        }

        public static RefAsync<T> Init()
        {
            return new RefAsync<T>();
        }
    }
}