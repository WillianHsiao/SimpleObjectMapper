using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using ObjectMapper.AdoNetToModel;

namespace ObjectMapper
{
    public static class DataReaderMapper
    {
        #region DataReaderToModel
        /// <summary>
        /// 特定資料欄轉換成特定值
        /// </summary>
        /// <typeparam name="T">值型別</typeparam>
        /// <param name="dataReader">DataReader</param>
        /// <param name="columnName">欄位名稱</param>
        /// <returns></returns>
        public static T MapValue<T>(this DbDataReader dataReader, string columnName)
        {
            return dataReader.MapToValue<T>(columnName);
        }

        /// <summary>
        /// 特定資料欄轉換成特定值(非同步)
        /// </summary>
        /// <typeparam name="T">值型別</typeparam>
        /// <param name="dataReader">DataReader</param>
        /// <param name="columnName">欄位名稱</param>
        /// <returns></returns>
        public static async Task<T> MapValueAsync<T>(this DbDataReader dataReader, string columnName)
        {
            return await dataReader.MapToValueAsync<T>(columnName);
        }

        /// <summary>
        /// DataReader轉換成陣列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static List<T> MapValueList<T>(this DbDataReader dataReader, string columnName)
        {
            return dataReader.MapToList<T>(columnName);
        }

        /// <summary>
        /// DataReader轉換成陣列(非同步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static Task<List<T>> MapValueListAsync<T>(this DbDataReader dataReader, string columnName)
        {
            return dataReader.MapToListAsync<T>(columnName);
        }
        /// <summary>
        /// 資料列轉換成物件
        /// </summary>
        /// <typeparam name="T">物件型別</typeparam>
        /// <param name="dataReader">資料列</param>
        /// <returns></returns>
        public static T MapModel<T>(this DbDataReader dataReader) where T : class
        {
            return dataReader.MapToValue<T>();
        }

        /// <summary>
        /// 資料列轉換成物件(非同步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static async Task<T> MapModelAsync<T>(this DbDataReader dataReader) where T : class
        {
            return await dataReader.MapToValueAsync<T>();
        }

        /// <summary>
        /// 資料表轉換成陣列
        /// </summary>
        /// <typeparam name="T">陣列型別</typeparam>
        /// <param name="dataReader">資料表</param>
        /// <returns></returns>
        public static List<T> MapModelList<T>(this DbDataReader dataReader) where T : class
        {
            return dataReader.MapToList<T>();
        }

        /// <summary>
        /// 資料表轉換成陣列(非同步)
        /// </summary>
        /// <typeparam name="T">陣列型別</typeparam>
        /// <param name="dataReader">資料表</param>
        /// <returns></returns>
        public static async Task<List<T>> MapModelListAsync<T>(this DbDataReader dataReader) where T : class
        {
            return await dataReader.MapToListAsync<T>();
        }
        #endregion
    }
}
