using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleObjectMapper.ModelToModel;

namespace SimpleObjectMapper
{
    /// <summary>
    /// 物件轉換
    /// </summary>
    public static class ModelMapper
    {
        /// <summary>
        /// 物件轉換
        /// </summary>
        /// <typeparam name="TTarget">目標型別</typeparam>
        /// <typeparam name="TSource">來源型別</typeparam>
        /// <param name="source">來源物件</param>
        /// <returns></returns>
        public static TTarget Map<TTarget, TSource>(this TSource source) where TTarget : class
        {
            return source.MapToValue<TTarget>();
        }

        /// <summary>
        /// 物件轉換(非同步)
        /// </summary>
        /// <typeparam name="TTarget">目標型別</typeparam>
        /// <typeparam name="TSource">來源型別</typeparam>
        /// <param name="source">來源物件</param>
        /// <returns></returns>
        public static async Task<TTarget> MapAsync<TTarget, TSource>(this TSource source) where TTarget : class
        {
            return await source.MapToValueAsync<TTarget>();
        }

        /// <summary>
        /// 陣列轉換
        /// </summary>
        /// <typeparam name="TTarget">目標型別</typeparam>
        /// <typeparam name="TSource">來源型別</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<TTarget> MapList<TTarget, TSource>(this List<TSource> source) where TTarget : class
        {
            if (source == null)
            {
                throw new NullReferenceException("資料來源為Null");
            }
            return source.MapToList<TTarget>();
        }

        /// <summary>
        /// 陣列轉換(非同步)
        /// </summary>
        /// <typeparam name="TTarget">目標型別</typeparam>
        /// <typeparam name="TSource">來源型別</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static async Task<List<TTarget>> MapListAsync<TTarget, TSource>(this List<TSource> source) where TTarget : class
        {
            if (source == null)
            {
                throw new NullReferenceException("資料來源為Null");
            }
            return await source.MapToListAsync<TTarget>();
        }
    }
}
