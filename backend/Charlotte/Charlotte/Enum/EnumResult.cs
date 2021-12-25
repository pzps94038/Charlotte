using System.ComponentModel;

namespace RonCoffee.Enum
{
    public enum EnumResult
    {

        [Description("取得資料成功")]
        Success,
        [Description("找不到資料")]
        NotFound,
        [Description("取得資料失敗")]
        Fail
    }
}
