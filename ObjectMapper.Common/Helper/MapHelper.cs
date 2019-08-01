using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace ObjectMapper.Common.Helper
{
    public static class MapHelper
    {
        public static object ConvertValueFromDataRow(this PropertyInfo property, DataRow dataRow, string customName = null)
        {
            var typeConverter = TypeDescriptor.GetConverter(property.PropertyType);
            if (customName != null)
            {
                if (dataRow.Table.Columns.Contains(customName))
                {
                    return typeConverter.ConvertFromString(dataRow[customName].ToString());
                }
            }

            if (dataRow.Table.Columns.Contains(property.Name))
            {
                return typeConverter.ConvertFromString(dataRow[property.Name].ToString());
            }

            return null;
        }
    }
}
