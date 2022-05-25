using System.Net;

namespace Charlotte.Model
{
    /// <summary>
    /// 回傳訊息
    /// </summary>
    public class ResultModel
    {
        public HttpStatusCode Code { get; set; }
        public string? Message { get; set; }
    }

    /// <summary>
    /// 回傳資料跟訊息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultModel
        <T>
    {
        public HttpStatusCode Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
