using System.ComponentModel;
using System.Reflection;

namespace Charlotte.Helper
{
    public static class EnumHelper
    {
        /// <summary>
        /// 取列舉描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            return enumValue.GetType()
                       .GetMember(enumValue.ToString())
                       .First()
                       .GetCustomAttribute<DescriptionAttribute>()?
                       .Description ?? string.Empty;
        }
    }
}
