using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WizardAi.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            Type enumType = enumValue.GetType();
            string enumName = Enum.GetName(enumType, enumValue);

            FieldInfo field = enumType.GetField(enumName);

            DisplayAttribute displayAttribute = field.GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.Name ?? enumName;
        }
    }
}
