
namespace Books.Helpers
{
    public class MathodResult<T>
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
