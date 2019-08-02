using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using ObjectMapper.Common.Attribute;
using ObjectMapper.Common.Helper;

namespace ObjectMapper.AdoNetToModel
{
    public static class DataReaderMapper
    {
        /// <summary>
        /// 資料讀取轉成陣列
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataReader">資料讀取</param>
        /// <returns></returns>
        public static List<T> ReadToList<T>(this DbDataReader dataReader)
        {
            var result = new List<T>();
            while (dataReader.Read())
            {
                result.Add(ReaderGetInstance<T>(dataReader));
            }

            return result;
        }

        /// <summary>
        /// 非同步資料讀取轉成陣列
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataReader">資料讀取</param>
        /// <returns></returns>
        public static Task<List<T>> ReadToListAsync<T>(this DbDataReader dataReader)
        {
            return Task.FromResult(ReadToList<T>(dataReader));
        }

        /// <summary>
        /// 自動比對ColumnName與PropertyName並回傳指定類型的物件
        /// </summary>
        /// <typeparam name="T">轉換型別</typeparam>
        /// <param name="dataReader">資料讀取</param>
        /// <returns></returns>
        private static T ReaderGetInstance<T>(this DbDataReader dataReader)
        {
            var result = Activator.CreateInstance<T>();
            var type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                var mapName = property.Name;
                var colAttrs = property.GetCustomAttributes(typeof(MapSettingAttribute),
                    false);
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
    }
}
