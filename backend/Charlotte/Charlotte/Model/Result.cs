namespace Charlotte.Model
{
    public class Result<T>
    {
        public int ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public T? Data { get; set; }   
    }
}
