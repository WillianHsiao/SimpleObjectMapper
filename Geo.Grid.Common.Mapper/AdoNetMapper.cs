using ObjectMapper.AdoNetToModel;
using System.Collections.Generic;
using System.Data;

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
    }
}
