using SimpleObjectMapper.AdoNetToModel;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SimpleObjectMapper
{
    /// <summary>
    /// AdoNet轉換
    /// </summary>
    public static class DataTableMapper
    {
        #region DataTableToModel
        /// <summary>
        /// 特定資料欄轉換成特定值
        /// </summary>
        /// <typeparam name="T">值型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <param name="columnName">資料欄名稱</param>
        /// <returns></returns>
        public static T MapValue<T>(this DataRow dataRow, string columnName)
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
        public static async Task<T> MapValueAsync<T>(this DataRow dataRow, string columnName)
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
        public static List<T> MapValueList<T>(this DataTable dataTable, string columnName)
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
        public static async Task<List<T>> MapValueListAsync<T>(this DataTable dataTable, string columnName)
        {
            return await dataTable.MapToListAsync<T>();
        }

        /// <summary>
        /// 資料列轉換成物件
        /// </summary>
        /// <typeparam name="T">物件型別</typeparam>
        /// <param name="dataRow">資料列</param>
        /// <returns></returns>
        public static T MapModel<T>(this DataRow dataRow) where T : class
        {
            return dataRow.MapToValue<T>();
        }

        /// <summary>
        /// 資料列轉換成物件(非同步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public static async Task<T> MapModelAsync<T>(this DataRow dataRow) where T : class
        {
            return await dataRow.MapToValueAsync<T>();
        }

        /// <summary>
        /// 資料表轉換成陣列
        /// </summary>
        /// <typeparam name="T">陣列型別</typeparam>
        /// <param name="dataTable">資料表</param>
        /// <returns></returns>
        public static List<T> MapModelList<T>(this DataTable dataTable) where T : class
        {
            return dataTable.MapToList<T>();
        }

        /// <summary>
        /// 資料表轉換成陣列(非同步)
        /// </summary>
        /// <typeparam name="T">陣列型別</typeparam>
        /// <param name="dataTable">資料表</param>
        /// <returns></returns>
        public static async Task<List<T>> MapModelListAsync<T>(this DataTable dataTable) where T : class
        {
            return await dataTable.MapToListAsync<T>();
        }
        #endregion

        
    }
}
