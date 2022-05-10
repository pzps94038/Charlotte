namespace Charlotte.Services.CustomizeException
{
    class CustomizeException
    {
        public class NotFoundException : Exception 
        {
            public NotFoundException() :base("找不到資料"){}
        }

        public class AESConvertDataException : Exception
        {
            public AESConvertDataException() : base("AES轉換資料失敗") { }
        }
    }
}
