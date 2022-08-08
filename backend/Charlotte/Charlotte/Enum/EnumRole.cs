using System.ComponentModel;

namespace Charlotte.Enum
{
    public enum EnumRole
    {
        // 管理平台
        [Description("ManagerUser")]
        ManagerUser,
        // 一般使用者
        [Description("User")]
        User,
    }
}
