using System.ComponentModel;

namespace Charlotte.Enum
{
    public enum EnumResult
    {
        [Description("取得資料成功")]
        Success,
        [Description("取得資料失敗")]
        Fail,
        [Description("找不到資料")]
        NotFound,
        [Description("新增資料成功")]
        CreateSuccess,
        [Description("新增資料失敗")]
        CreateFail,
        [Description("修改資料成功")]
        ModifySuccess,
        [Description("修改資料失敗")]
        ModifyFail,
        [Description("刪除資料成功")]
        DeleteSuccess,
        [Description("刪除資料失敗")]
        DeleteFail,
    }
}
