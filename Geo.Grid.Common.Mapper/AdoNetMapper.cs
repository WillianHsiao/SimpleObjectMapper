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
        /// <summary>
        /// 特定資料欄轉換成特定值
        /// </summary>
        /// <typeparam name="T">值型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        public static T ToSingleValue<T>(this DataRow dataRow, string columnName)
        {
            return dataRow.MapToObject<T>(columnName);
        }

        /// <summary>
        /// 特定資料欄轉換成特定值(非同步)
        /// </summary>
        /// <typeparam name="T">值型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        public static async Task<T> ToSingleAsync<T>(this DataRow dataRow, string columnName)
        {
            return await dataRow.MapToObjectAsync<T>(columnName);
        }

        /// <summary>
        /// 資料表轉換為單一物件陣列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dataTable, string columnName)
        {
            return dataTable.MapToList<T>(columnName);
        }

        /// <summary>
        /// 資料列轉換成物件
        /// </summary>
        /// <typeparam name="T">物件型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <returns></returns>
        public static T ToModel<T>(this DataRow dataRow) where T : class
        {
            return dataRow.MapToModel<T>();
        }

        /// <summary>
        /// 資料表轉換成陣列
        /// </summary>
        /// <typeparam name="T">陣列型別</typeparam>
        /// <param name="dataTable">資料表</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dataTable) where T : class
        {
            return dataTable.MapToList<T>();
        }

        /// <summary>
        /// 特定資料欄轉換成特定值
        /// </summary>
        /// <typeparam name="T">值型別</typeparam>
        /// <param name="dataReader">DataReader</param>
        /// <param name="columnName">欄位名稱</param>
        /// <returns></returns>
        public static T ToSingleValue<T>(this DbDataReader dataReader, string columnName)
        {
            return dataReader.MapToObject<T>(columnName);
        }

        /// <summary>
        /// 特定資料欄轉換成特定值(非同步)
        /// </summary>
        /// <typeparam name="T">值型別</typeparam>
        /// <param name="dataReader">DataReader</param>
        /// <param name="columnName">欄位名稱</param>
        /// <returns></returns>
        public static async Task<T> ToSingleValueAsync<T>(this DbDataReader dataReader, string columnName)
        {
            return await dataReader.MapToObjectAsync<T>(columnName);
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
    }
}
