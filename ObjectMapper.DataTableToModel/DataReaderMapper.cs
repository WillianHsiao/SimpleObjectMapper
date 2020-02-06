using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using SimpleObjectMapper.Common.Attribute;
using SimpleObjectMapper.Common.Exception;
using SimpleObjectMapper.Common.Helper;

namespace SimpleObjectMapper.AdoNetToModel
{
    /// <summary>
    /// DataReader轉換
    /// </summary>
    public static class DataReaderMapper
    {
        /// <summary>
        /// 資料列轉換單一物件
        /// </summary>
        /// <typeparam name="T">目標型別</typeparam>
        /// <param name="dataReader">資料列</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        public static T MapToValue<T>(this DbDataReader dataReader, string columnName = null)
        {
            if (dataReader.Read())
            {
                if (typeof(T).IsClass)
                {
                    return dataReader.MapToModel<T>();
                }

                if (string.IsNullOrWhiteSpace(columnName))
                {
                    throw new NameMissException();
                }

                return dataReader.MapToSingleValue<T>(columnName);
            }

            return default(T);
        }

        /// <summary>
        /// 資料列轉換單一物件(非同步)
        /// </summary>
        /// <typeparam name="T">目標型別</typeparam>
        /// <param name="dataReader">資料列</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        public static async Task<T> MapToValueAsync<T>(this DbDataReader dataReader, string columnName = null)
        {
            if (await dataReader.ReadAsync())
            {
                if (typeof(T).IsClass)
                {
                    return await dataReader.MapToModelAsync<T>();
                }

                if (string.IsNullOrWhiteSpace(columnName))
                {
                    throw new NameMissException();
                }

                return await dataReader.MapToSingleValueAsync<T>(columnName);
            }

            return default(T);
        }

        /// <summary>
        /// 資料表轉換物件陣列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static List<T> MapToList<T>(this DbDataReader dataReader, string columnName = null)
        {
            if (typeof(T).IsClass)
            {
                return dataReader.MapToModelList<T>();
            }

            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new NameMissException();
            }
            return dataReader.MapToSingleValueList<T>(columnName);
        }

        /// <summary>
        /// 資料表轉換物件陣列(非同步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static async Task<List<T>> MapToListAsync<T>(this DbDataReader dataReader, string columnName = null)
        {
            if (typeof(T).IsClass)
            {
                return await dataReader.MapToModelListAsync<T>();
            }

            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new NameMissException();
            }
            return await dataReader.MapToSingleValueListAsync<T>(columnName);
        }
        /// <summary>
        /// 特定欄位轉換
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataReader">DataReader</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        private static T MapToSingleValue<T>(this DbDataReader dataReader, string columnName)
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
                throw;
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
        private static async Task<T> MapToSingleValueAsync<T>(this DbDataReader dataReader, string columnName)
        {
            return await Task.FromResult(dataReader.MapToSingleValue<T>(columnName));
        }

        /// <summary>
        /// 資料讀取轉成陣列
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataReader">資料讀取</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        private static List<T> MapToSingleValueList<T>(this DbDataReader dataReader, string columnName)
        {
            var result = new List<T>();
            while (dataReader.Read())
            {
                result.Add(dataReader.MapToSingleValue<T>(columnName));
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
        private static async Task<List<T>> MapToSingleValueListAsync<T>(this DbDataReader dataReader, string columnName)
        {
            var result = new List<T>();
            while (await dataReader.ReadAsync())
            {
                result.Add(await dataReader.MapToSingleValueAsync<T>(columnName));
            }

            return result;
        }

        /// <summary>
        /// 資料表轉換陣列
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataReader">Data Reader</param>
        /// <returns></returns>
        private static List<T> MapToModelList<T>(this DbDataReader dataReader)
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
        private static async Task<List<T>> MapToModelListAsync<T>(this DbDataReader dataReader)
        {
            var result = new List<T>();
            while (await dataReader.ReadAsync())
            {
                result.Add(await dataReader.MapToModelAsync<T>());
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
        private static async Task<T> MapToModelAsync<T>(this DbDataReader dataReader)
        {
            return await Task.FromResult(dataReader.MapToModel<T>());
        }
    }
}
