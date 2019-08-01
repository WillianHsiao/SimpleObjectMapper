using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using ObjectMapper.Common.Attribute;
using ObjectMapper.Common.Exception;
using ObjectMapper.Common.Helper;

namespace ObjectMapper.AdoNetToModel
{
    public static class DataTableMapper
    {
        /// <summary>
        /// 特定欄位轉換
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static T MapToObject<T>(this DataRow dataRow, string columnName)
        {
            T result;
            try
            {
                if (!dataRow.Table.Columns.Contains(columnName))
                {
                    throw new WrongNameException();
                }

                var type = typeof(T);
                var typeConverter = TypeDescriptor.GetConverter(type);
                try
                {
                    var value = typeConverter.ConvertFromString(dataRow[columnName].ToString());
                    result = (T) value;
                }
                catch (NotSupportedException ex)
                {
                    var wrongTypeEx = new WrongTypeException();
                    wrongTypeEx.SourcePropertyType = dataRow[columnName].GetType();
                    wrongTypeEx.TargetPropertyType = type;
                    throw wrongTypeEx;
                }
            }
            catch (WrongNameException ex)
            {
                ex.TargetPropertyName = columnName;
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 轉為單一屬性的陣列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static List<T> MapToList<T>(this DataTable dataTable, string columnName)
        {
            var result = new List<T>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                result.Add(dataRow.MapToObject<T>(columnName));
            }

            return result;
        }

        /// <summary>
        /// 資料列轉換單一物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public static T MapToModel<T>(this DataRow dataRow) where T : class
        {
            var result = Activator.CreateInstance<T>();
            var type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                try
                {
                    var mapName = property.Name;
                    var colAttrs = property.GetCustomAttributes(typeof(MapSettingAttribute), false);
                    var isIgnore = false;
                    if (colAttrs.Any())
                    {
                        var mapSetting = (MapSettingAttribute) colAttrs.First();
                        if (!string.IsNullOrWhiteSpace(mapSetting.Name))
                        {
                            mapName = mapSetting.Name;
                        }
                        isIgnore = mapSetting.Ignore;
                    }

                    if (isIgnore)
                    {
                        continue;
                    }
                    property.SetValue(result, property.ConvertValueFromDataRow(dataRow, mapName));
                }
                catch
                {
                    // ignored
                }
            }

            return result;
        }

        /// <summary>
        /// 資料表轉換陣列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<T> MapToList<T>(this DataTable dataTable) where T : class
        {
            var result = new List<T>();
            foreach (DataRow dr in dataTable.Rows)
            {
                result.Add(dr.MapToModel<T>());
            }

            return result;
        }
    }
}
