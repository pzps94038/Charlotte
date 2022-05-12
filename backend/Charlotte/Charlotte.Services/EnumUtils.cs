using System.ComponentModel;
using System.Reflection;

namespace Charlotte.Services
{
    public class EnumUtils
    {
        /// <summary>
        /// 取列舉描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(Enum enumValue)
        {
            return enumValue.GetType()
                       .GetMember(enumValue.ToString())
                       .First()
                       .GetCustomAttribute<DescriptionAttribute>()?
                       .Description ?? string.Empty;
        }
    }
}
