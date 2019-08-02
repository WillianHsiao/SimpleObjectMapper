using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using ObjectMapper.Common.Attribute;
using ObjectMapper.Common.Exception;
using ObjectMapper.Common.Helper;

namespace ObjectMapper.AdoNetToModel
{
    /// <summary>
    /// DataReader轉換
    /// </summary>
    public static class DataReaderMapper
    {
        /// <summary>
        /// 特定欄位轉換
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataReader">DataReader</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        public static T MapToObject<T>(this DbDataReader dataReader, string columnName)
        {
            T result;
            try
            {
                if (!dataReader.HasColumn(columnName))
                {
                    throw new WrongNameException();
                }

                result = (T) dataReader.ConvertValue(typeof(T), columnName);
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
        /// <param name="dataReader">DataReader</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        public static async Task<T> MapToObjectAsync<T>(this DbDataReader dataReader, string columnName)
        {
            return await Task.FromResult(dataReader.MapToObject<T>(columnName));
        }

        /// <summary>
        /// 資料讀取轉成陣列
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataReader">資料讀取</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        public static List<T> MapToList<T>(this DbDataReader dataReader, string columnName)
        {
            var result = new List<T>();
            while (dataReader.Read())
            {
                result.Add(dataReader.MapToObject<T>(columnName));
            }

            return result;
        }

        /// <summary>
        /// 非同步資料讀取轉成陣列
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataReader">資料讀取</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        public static async Task<List<T>> MapToListAsync<T>(this DbDataReader dataReader, string columnName)
        {
            var result = new List<T>();
            while (dataReader.Read())
            {
                result.Add(await dataReader.MapToObjectAsync<T>(columnName));
            }
            return result;
        }

        /// <summary>
        /// 自動比對ColumnName與PropertyName並回傳指定類型的物件
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataReader">資料讀取</param>
        /// <returns></returns>
        private static T MapToModel<T>(this DbDataReader dataReader)
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
                    mapName = mapSetting.Name;
                    isIgnore = mapSetting.Ignore;
                }

                if (isIgnore)
                {
                    continue;
                }
                property.SetValue(result, property.ConvertValueFromDataReader(dataReader, mapName));
            }

            return result;
        }

        /// <summary>
        /// 自動比對ColumnName與PropertyName並回傳指定類型的物件(非同步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        private static Task<T> MapToModelAsync<T>(this DbDataReader dataReader)
        {
            return Task.FromResult(dataReader.MapToModel<T>());
        }

        /// <summary>
        /// 資料表轉換陣列
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataReader">Data Reader</param>
        /// <returns></returns>
        public static List<T> MapToList<T>(this DbDataReader dataReader) where T : class
        {
            var result = new List<T>();
            while (dataReader.Read())
            {
                result.Add(dataReader.MapToModel<T>());
            }

            return result;
        }

        /// <summary>
        /// 資料表轉換陣列(非同步)
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataReader">資料表</param>
        /// <returns></returns>
        public static async Task<List<T>> MapToListAsync<T>(this DbDataReader dataReader) where T : class
        {
            return await Task.FromResult(dataReader.MapToList<T>());
        }
    }
}
