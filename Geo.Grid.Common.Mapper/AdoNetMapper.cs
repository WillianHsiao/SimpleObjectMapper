using ObjectMapper.AdoNetToModel;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Geo.Grid.Common.Mapper
{
    /// <summary>
    /// AdoNet轉換
    /// </summary>
    public static class AdoNetMapper
    {
        #region DataTableToModel
        /// <summary>
        /// 特定資料欄轉換成特定值
        /// </summary>
        /// <typeparam name="T">值型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        public static T ToValue<T>(this DataRow dataRow, string columnName)
        {
            return dataRow.MapToValue<T>(columnName);
        }

        /// <summary>
        /// 特定資料欄轉換成特定值(非同步)
        /// </summary>
        /// <typeparam name="T">值型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        public static async Task<T> ToValueAsync<T>(this DataRow dataRow, string columnName)
        {
            return await dataRow.MapToValueAsync<T>(columnName);
        }

        /// <summary>
        /// 資料表轉換為單一物件陣列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static List<T> ToValueList<T>(this DataTable dataTable, string columnName)
        {
            return dataTable.MapToList<T>(columnName);
        }

        /// <summary>
        /// 資料表轉換為單一物件陣列(非同步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static async Task<List<T>> ToValueListAsync<T>(this DataTable dataTable, string columnName)
        {
            return await dataTable.MapToListAsync<T>();
        }

        /// <summary>
        /// 資料列轉換成物件
        /// </summary>
        /// <typeparam name="T">物件型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <returns></returns>
        public static T ToModel<T>(this DataRow dataRow) where T : class
        {
            return dataRow.MapToValue<T>();
        }

        /// <summary>
        /// 資料列轉換成物件(非同步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public static async Task<T> ToModelAsync<T>(this DataRow dataRow) where T : class
        {
            return await dataRow.MapToValueAsync<T>();
        }

        /// <summary>
        /// 資料表轉換成陣列
        /// </summary>
        /// <typeparam name="T">陣列型別</typeparam>
        /// <param name="dataTable">資料表</param>
        /// <returns></returns>
        public static List<T> ToModelList<T>(this DataTable dataTable) where T : class
        {
            return dataTable.MapToList<T>();
        }

        /// <summary>
        /// 資料表轉換成陣列(非同步)
        /// </summary>
        /// <typeparam name="T">陣列型別</typeparam>
        /// <param name="dataTable">資料表</param>
        /// <returns></returns>
        public static async Task<List<T>> ToModelListAsync<T>(this DataTable dataTable) where T : class
        {
            return await dataTable.MapToListAsync<T>();
        }
        #endregion

        #region DataReaderToModel
        /// <summary>
        /// 特定資料欄轉換成特定值
        /// </summary>
        /// <typeparam name="T">值型別</typeparam>
        /// <param name="dataReader">DataReader</param>
        /// <param name="columnName">欄位名稱</param>
        /// <returns></returns>
        public static T ToValue<T>(this DbDataReader dataReader, string columnName)
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
        public static async Task<T> ToValueAsync<T>(this DbDataReader dataReader, string columnName)
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
        public static List<T> ToList<T>(this DbDataReader dataReader, string columnName)
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
        public static Task<List<T>> ToListAsync<T>(this DbDataReader dataReader, string columnName)
        {
            return dataReader.MapToListAsync<T>(columnName);
        }
        #endregion
    }
}
