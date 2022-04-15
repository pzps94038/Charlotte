using System.Runtime.Serialization;

namespace Charlotte.CustomizeException
{
    class CustomizeException
    {
        public class NotFoundException : Exception 
        {
            public NotFoundException() :base("找不到資料"){}
        }
    }
}
