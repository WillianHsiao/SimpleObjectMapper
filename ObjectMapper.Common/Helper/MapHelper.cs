using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using ObjectMapper.Common.Exception;

namespace ObjectMapper.Common.Helper
{
    public static class MapHelper
    {
        public static object ConvertValueFromDataRow(this PropertyInfo property, DataRow dataRow, string customName = null)
        {
            object result = null;
            if (!string.IsNullOrWhiteSpace(customName))
            {
                result = ConvertValue(property.PropertyType, dataRow, customName);
            }

            if (result == null)
            {
                result = ConvertValue(property.PropertyType, dataRow, property.Name);
            }

            return result;
        }

        private static object ConvertValue(Type type, DataRow dataRow, string columnName)
        {
            var typeConverter = TypeDescriptor.GetConverter(type);
            try
            {
                if (dataRow.Table.Columns.Contains(columnName))
                {
                    return typeConverter.ConvertFromString(dataRow[columnName].ToString());
                }

                return null;
            }
            catch (NotSupportedException ex)
            {
                var wrongTypeEx = new WrongTypeException();
                wrongTypeEx.SourcePropertyType = dataRow[columnName].GetType();
                wrongTypeEx.TargetPropertyType = type;
                throw wrongTypeEx;
            }
        }
    }
}
