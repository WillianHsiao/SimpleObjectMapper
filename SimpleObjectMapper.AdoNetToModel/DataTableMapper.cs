using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SimpleObjectMapper.Common.Attribute;
using SimpleObjectMapper.Common.Exception;
using SimpleObjectMapper.Common.Helper;

namespace SimpleObjectMapper.AdoNetToModel
{
    /// <summary>
    /// DataTable轉換
    /// </summary>
    public static class DataTableMapper
    {
        /// <summary>
        /// 資料列轉換單一物件
        /// </summary>
        /// <typeparam name="T">目標型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        public static T MapToValue<T>(this DataRow dataRow, string columnName = null)
        {
            if (typeof(T).IsClass)
            {
                return dataRow.MapToModel<T>();
            }

            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new NameMissException();
            }

            return dataRow.MapToSingleValue<T>(columnName);
        }

        /// <summary>
        /// 資料列轉換單一物件(非同步)
        /// </summary>
        /// <typeparam name="T">目標型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        public static async Task<T> MapToValueAsync<T>(this DataRow dataRow, string columnName = null)
        {
            if (typeof(T).IsClass)
            {
                return await dataRow.MapToModelAsync<T>();
            }

            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new NameMissException();
            }

            return await dataRow.MapToSingleValueAsync<T>(columnName);
        }

        /// <summary>
        /// 資料表轉換物件陣列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static List<T> MapToList<T>(this DataTable dataTable, string columnName = null)
        {
            if (typeof(T).IsClass)
            {
                return dataTable.MapToModelList<T>();
            }

            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new NameMissException();
            }
            return dataTable.MapToSingleValueList<T>(columnName);
        }

        /// <summary>
        /// 資料表轉換物件陣列(非同步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static async Task<List<T>> MapToListAsync<T>(this DataTable dataTable, string columnName = null)
        {
            if (typeof(T).IsClass)
            {
                return await dataTable.MapToModelListAsync<T>();
            }

            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new NameMissException();
            }
            return await dataTable.MapToSingleValueListAsync<T>(columnName);
        }

        /// <summary>
        /// 特定欄位轉換
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        private static T MapToSingleValue<T>(this DataRow dataRow, string columnName)
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
                throw;
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
        private static async Task<T> MapToSingleValueAsync<T>(this DataRow dataRow, string columnName)
        {
            return await Task.FromResult(dataRow.MapToSingleValue<T>(columnName));
        }

        /// <summary>
        /// 轉為單一屬性的陣列
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataTable">資料表</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        private static List<T> MapToSingleValueList<T>(this DataTable dataTable, string columnName)
        {
            var result = new List<T>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                result.Add(dataRow.MapToSingleValue<T>(columnName));
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
        private static async Task<List<T>> MapToSingleValueListAsync<T>(this DataTable dataTable, string columnName)
        {
            var result = new List<T>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                result.Add(await dataRow.MapToSingleValueAsync<T>(columnName));
            }

            return result;
        }

        /// <summary>
        /// 資料表轉換陣列
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataTable">資料表</param>
        /// <returns></returns>
        private static List<T> MapToModelList<T>(this DataTable dataTable)
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
        private static async Task<List<T>> MapToModelListAsync<T>(this DataTable dataTable)
        {
            var result = new List<T>();
            foreach (DataRow dr in dataTable.Rows)
            {
                result.Add(await dr.MapToModelAsync<T>());
            }

            return result;
        }

        /// <summary>
        /// 資料列轉換單一物件
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <returns></returns>
        private static T MapToModel<T>(this DataRow dataRow)
        {
            var result = Activator.CreateInstance<T>();
            var type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                if (!property.CanWrite)
                {
                    continue;
                }
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
        private static async Task<T> MapToModelAsync<T>(this DataRow dataRow)
        {
            return await Task.FromResult(dataRow.MapToModel<T>());
        }
    }
}
