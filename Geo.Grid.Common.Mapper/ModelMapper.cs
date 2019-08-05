using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ObjectMapper.Common.Exception;
using ObjectMapper.ModelToModel;

namespace Geo.Grid.Common.Mapper
{
    /// <summary>
    /// 物件轉換
    /// </summary>
    public static class ModelMapper
    {
        /// <summary>
        /// 物件轉換
        /// </summary>
        /// <typeparam name="T">目標型別</typeparam>
        /// <param name="source">來源物件</param>
        /// <returns></returns>
        public static T ToValue<T>(this object source) where T : class
        {
            if (source is IList)
            {
                throw new WrongTypeException("此方法無法轉換陣列物件，請使用ToList");
            }
            return source.MapToValue<T>();
        }

        /// <summary>
        /// 物件轉換(非同步)
        /// </summary>
        /// <typeparam name="T">目標型別</typeparam>
        /// <param name="source">來源物件</param>
        /// <returns></returns>
        public static async Task<T> ToValueAsync<T>(this object source) where T : class
        {
            if (source is IList)
            {
                throw new WrongTypeException("此方法無法轉換陣列物件，請使用ToList");
            }
            return await source.MapToValueAsync<T>();
        }

        /// <summary>
        /// 陣列轉換
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this object source) where T : class
        {
            if (source == null)
            {
                throw new NullReferenceException("資料來源為Null");
            }
            if (source is IList)
            {
                return source.MapToList<T>();
            }
            throw new WrongTypeException("此方法無法轉換單個物件，請使用ToValue。");
        }

        /// <summary>
        /// 陣列轉換(非同步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static async Task<List<T>> ToListAsync<T>(this object source) where T : class
        {
            if (source is IList)
            {
                return await source.MapToListAsync<T>();
            }
            throw new WrongTypeException("此方法無法轉換單個物件，請使用ToValue。");
        }
    }
}
