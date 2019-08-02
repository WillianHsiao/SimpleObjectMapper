using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Reflection;
using ObjectMapper.Common.Exception;

namespace ObjectMapper.Common.Helper
{
    public static class MapHelper
    {
        /// <summary>
        /// 物件轉物件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="T"></param>
        /// <returns></returns>
        public static T ConvertValue<T>(this object source)
        {
            var targetType = typeof(T);
            var typeConverter = TypeDescriptor.GetConverter(targetType);
            try
            {
                return (T) typeConverter.ConvertFromString(source.ToString());
            }
            catch (FormatException ex)
            {
                var wrongTypeEx = new WrongTypeException();
                wrongTypeEx.SourcePropertyType = source.GetType();
                wrongTypeEx.TargetPropertyType = targetType;
                throw wrongTypeEx;
            }
        }

        /// <summary>
        /// 資料列轉換特定欄位(自訂名稱)
        /// </summary>
        /// <param name="property"></param>
        /// <param name="dataRow"></param>
        /// <param name="customName"></param>
        /// <returns></returns>
        public static object ConvertValueFromDataRow(this PropertyInfo property, DataRow dataRow,
            string customName = null)
        {
            object result;
            if (!string.IsNullOrWhiteSpace(customName) && dataRow.Table.Columns.Contains(customName))
            {
                result = dataRow.ConvertValue(property.PropertyType, customName);
            }
            else
            {
                result = dataRow.ConvertValue(property.PropertyType, property.Name);
            }

            return result;
        }

        /// <summary>
        /// 資料列轉換特定欄位
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dataRow"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static object ConvertValue(this DataRow dataRow, Type type, string columnName)
        {
            var typeConverter = TypeDescriptor.GetConverter(type);
            try
            {
                return typeConverter.ConvertFromString(dataRow[columnName].ToString());
            }
            catch (FormatException ex)
            {
                var wrongTypeEx = new WrongTypeException();
                wrongTypeEx.SourcePropertyType = dataRow[columnName].GetType();
                wrongTypeEx.TargetPropertyType = type;
                throw wrongTypeEx;
            }
        }

        /// <summary>
        /// 資料列轉換特定欄位(自訂名稱)
        /// </summary>
        /// <param name="property"></param>
        /// <param name="dataReader"></param>
        /// <param name="customName"></param>
        /// <returns></returns>
        public static object ConvertValueFromDataReader(this PropertyInfo property, DbDataReader dataReader,
            string customName = null)
        {
            object result;
            if (!string.IsNullOrWhiteSpace(customName) && dataReader.HasColumn(customName))
            {
                result = dataReader.ConvertValue(property.PropertyType, customName);
            }
            else
            {
                result = dataReader.ConvertValue(property.PropertyType, property.Name);
            }

            return result;
        }

        /// <summary>
        /// 資料列轉換特定欄位
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="type"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static object ConvertValue(this DbDataReader dataReader, Type type, string columnName)
        {
            var typeConverter = TypeDescriptor.GetConverter(type);
            var colIndex = dataReader.GetOrdinal(columnName);
            try
            {
                if (!dataReader.IsDBNull(colIndex))
                {
                    return typeConverter.ConvertFromString(dataReader.GetValue(colIndex).ToString());
                }
            }
            catch (FormatException ex)
            {
                var wrongTypeEx = new WrongTypeException();
                wrongTypeEx.SourcePropertyType = dataReader.GetValue(colIndex).GetType();
                wrongTypeEx.TargetPropertyType = type;
                throw wrongTypeEx;
            }

            return null;
        }

        /// <summary>
        /// Check data reader has column.
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static bool HasColumn(this DbDataReader dr, string columnName)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
