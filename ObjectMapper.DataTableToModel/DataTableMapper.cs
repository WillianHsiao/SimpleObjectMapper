using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ObjectMapper.Common.Attribute;
using ObjectMapper.Common.Exception;
using ObjectMapper.Common.Helper;

namespace ObjectMapper.AdoNetToModel
{
    /// <summary>
    /// DataTable轉換
    /// </summary>
    public static class DataTableMapper
    {
        /// <summary>
        /// 特定欄位轉換
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <param name="columnName">資料欄名稱</param>
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

                result = (T) dataRow.ConvertValue(typeof(T), columnName);
            }
            catch (WrongNameException ex)
            {
                ex.TargetPropertyName = columnName;
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 特定欄位轉換(非同步)
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        public static async Task<T> MapToObjectAsync<T>(this DataRow dataRow, string columnName)
        {
            return await Task.FromResult(dataRow.MapToObject<T>(columnName));
        }

        /// <summary>
        /// 轉為單一屬性的陣列
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataTable">資料表</param>
        /// <param name="columnName">資料欄名稱</param>
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
        /// 轉為單一屬性的陣列(非同步)
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataTable">資料表</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        public static async Task<List<T>> MapToListAsync<T>(this DataTable dataTable, string columnName)
        {
            var result = new List<T>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                result.Add(await dataRow.MapToObjectAsync<T>(columnName));
            }

            return result;
        }

        /// <summary>
        /// 資料列轉換單一物件
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <returns></returns>
        public static T MapToModel<T>(this DataRow dataRow) where T : class
        {
            var result = Activator.CreateInstance<T>();
            var type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                var mapName = property.Name;
                var colAttrs = property.GetCustomAttributes(typeof(MapSettingAttribute), false);
                var isIgnore = false;
                if (colAttrs.Any())
                {
                    var mapSetting = (MapSettingAttribute)colAttrs.First();
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

            return result;
        }

        /// <summary>
        /// 資料列轉換單一物件(非同步)
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <returns></returns>
        public static async Task<T> MapToModelAsync<T>(this DataRow dataRow) where T : class
        {
            return await Task.FromResult(dataRow.MapToModel<T>());
        }

        /// <summary>
        /// 資料表轉換陣列
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataTable">資料表</param>
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

        /// <summary>
        /// 資料表轉換陣列(非同步)
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataTable">資料表</param>
        /// <returns></returns>
        public static async Task<List<T>> MapToListAsync<T>(this DataTable dataTable) where T : class
        {
            return await Task.FromResult(dataTable.MapToList<T>());
        }
    }
}
