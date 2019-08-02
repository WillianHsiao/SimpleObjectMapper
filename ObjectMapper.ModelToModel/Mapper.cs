using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectMapper.Common.Attribute;
using ObjectMapper.Common.Exception;
using ObjectMapper.Common.Helper;

namespace ObjectMapper.ModelToModel
{
    /// <summary>
    /// 模組與模組轉換
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// 物件轉換
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T MapToValue<T>(this object source)
        {
            if (typeof(T).IsClass)
            {
                return source.MapToModel<T>();
            }

            return source.MapToSingleValue<T>();
        }

        /// <summary>
        /// 物件轉換(非同步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static async Task<T> MapToValueAsync<T>(this object source)
        {
            if (typeof(T).IsClass)
            {
                return await source.MapToModelAsync<T>();
            }

            return await source.MapToSingleValueAsync<T>();
        }

        /// <summary>
        /// 陣列轉陣列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public static List<T> MapToList<T>(this List<object> sourceList)
        {
            if (typeof(T).IsClass)
            {
                return sourceList.MapToSingleValueList<T>();
            }

            return sourceList.MapToModelList<T>();
        }

        /// <summary>
        /// 陣列轉陣列(非同步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public static async Task<List<T>> MapToListAsync<T>(this List<object> sourceList)
        {
            if (typeof(T).IsClass)
            {
                return await sourceList.MapToModelListAsync<T>();
            }

            return await sourceList.MapToSingleValueListAsync<T>();
        }

        /// <summary>
        /// 單元素陣列轉換
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        private static List<T> MapToSingleValueList<T>(this List<object> sourceList)
        {
            var result = new List<T>();
            foreach (var obj in sourceList)
            {
                result.Add(obj.MapToSingleValue<T>());
            }

            return result;
        }

        /// <summary>
        /// 單元素陣列轉換(非同步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        private static async Task<List<T>> MapToSingleValueListAsync<T>(this List<object> sourceList)
        {
            var result = new List<T>();
            foreach (var obj in sourceList)
            {
                result.Add(await obj.MapToSingleValueAsync<T>());
            }

            return result;
        }

        /// <summary>
        /// 物件之間轉換
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        private static T MapToSingleValue<T>(this object source)
        {
            return source.ConvertValue<T>();
        }

        /// <summary>
        /// 物件之間轉換(非同步)
        /// </summary>
        /// <typeparam name="T">目標型別</typeparam>
        /// <param name="source">來源陣列</param>
        /// <returns></returns>
        private static async Task<T> MapToSingleValueAsync<T>(this object source)
        {
            return await Task.FromResult(source.MapToSingleValue<T>());
        }

        /// <summary>
        /// 陣列轉換
        /// </summary>
        /// <typeparam name="T">目標型別</typeparam>
        /// <param name="sourceList">來源陣列</param>
        /// <returns></returns>
        private static List<T> MapToModelList<T>(this List<object> sourceList)
        {
            var result = new List<T>();
            foreach (var obj in sourceList)
            {
                result.Add(obj.MapToModel<T>());
            }

            return result;
        }

        /// <summary>
        /// 陣列轉換(非同步)
        /// </summary>
        /// <typeparam name="T">目標型別</typeparam>
        /// <param name="sourceList">來源陣列</param>
        /// <returns></returns>
        private static async Task<List<T>> MapToModelListAsync<T>(this List<object> sourceList)
        {
            var result = new List<T>();
            foreach (var obj in sourceList)
            {
                result.Add(await obj.MapToModelAsync<T>());
            }

            return result;
        }

        /// <summary>
        /// 物件與物件轉換
        /// </summary>
        /// <typeparam name="T">目標型別</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        private static T MapToModel<T>(this object source)
        {
            var result = Activator.CreateInstance<T>();
            var sourceType = source.GetType();
            var targetType = typeof(T);
            foreach (var targetProp in targetType.GetProperties())
            {
                var mapName = targetProp.Name;
                var colAttrs = targetProp.GetCustomAttributes(typeof(MapSettingAttribute), false);
                var isIgnore = false;
                if (colAttrs.Any())
                {
                    var mapSetting = (MapSettingAttribute) colAttrs.First();
                    if (!string.IsNullOrWhiteSpace(mapSetting.Name))
                    {
                        mapName = mapSetting.Name;
                    }

                    isIgnore = mapSetting.Ignore;
                }

                if (isIgnore)
                {
                    continue;
                }

                var sourceProp = sourceType.GetProperties()
                    .FirstOrDefault(p => p.Name == mapName || p.Name == targetProp.Name);
                if (sourceProp != null)
                {
                    targetProp.SetValue(result, sourceProp.GetValue(source));
                }
                else
                {
                    throw new WrongNameException
                    {
                        TargetPropertyName = targetProp.Name
                    };
                }
            }

            return result;
        }

        /// <summary>
        /// 物件與物件轉換(非同步)
        /// </summary>
        /// <typeparam name="T">目標型別</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        private static async Task<T> MapToModelAsync<T>(this object source)
        {
            return await Task.FromResult(source.MapToModel<T>());
        }
    }
}
