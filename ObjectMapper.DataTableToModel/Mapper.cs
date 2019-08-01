using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using ObjectMapper.Common.Attribute;

namespace ObjectMapper.AdoNetToModel
{
    public static class Mapper
    {
        /// <summary>
        /// 資料列轉換單一物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public static T MapToModel<T>(this DataRow dataRow) where T : new()
        {
            var result = new T();
            var type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                try
                {
                    var fieldName = property.Name;
                    var colAttrs = property.GetCustomAttributes(typeof(MapSettingAttribute), false);
                    var isIgnore = false;
                    if (colAttrs.Any())
                    {
                        var mapSetting = (MapSettingAttribute) colAttrs.First();
                        fieldName = mapSetting.Name;
                        isIgnore = mapSetting.Ignore;
                    }

                    if (isIgnore)
                    {
                        continue;
                    }
                    object value = default(T);
                    if (dataRow.Table.Columns.Contains(fieldName))
                    {
                        var typeConverter = TypeDescriptor.GetConverter(property.PropertyType);
                        value = typeConverter.
                            ConvertFromString(dataRow[fieldName].ToString());
                    }
                    property.SetValue(result, value);
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
        public static List<T> MapToList<T>(this DataTable dataTable) where T : new()
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
