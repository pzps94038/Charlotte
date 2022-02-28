using System.Net;

namespace Charlotte.Model
{
    /// <summary>
    /// 回傳訊息
    /// </summary>
    public class ResultModel
    {
        public HttpStatusCode code { get; set; }
        public string? message { get; set; }
    }

    /// <summary>
    /// 回傳資料跟訊息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultModel
        <T>
    {
        public HttpStatusCode code { get; set; }
        public string? message { get; set; }
        public T? data { get; set; }
    }

    /// <summary>
    /// 回傳多個資料跟訊息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultListModel<T> 
    {
        public HttpStatusCode code { get; set; }
        public string? message { get; set; }
        public List<T>? data { get; set; }
    }
}
