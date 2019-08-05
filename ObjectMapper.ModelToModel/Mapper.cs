using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectMapper.Common.Attribute;

namespace ObjectMapper.ModelToModel
{
    /// <summary>
    /// 模組或物件轉換
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// 物件轉換
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T MapToValue<T>(this object source) where T : class
        {
            return source.MapToModel<T>();
        }

        /// <summary>
        /// 物件轉換(非同步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static async Task<T> MapToValueAsync<T>(this object source) where T : class
        {
            return await source.MapToModelAsync<T>();
        }

        /// <summary>
        /// 陣列轉陣列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public static List<T> MapToList<T>(this object sourceList) where T : class
        {
            return sourceList.MapToModelList<T>();
        }

        /// <summary>
        /// 陣列轉陣列(非同步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public static async Task<List<T>> MapToListAsync<T>(this object sourceList) where T : class
        {
            return await sourceList.MapToModelListAsync<T>();
        }

        /// <summary>
        /// 陣列轉換
        /// </summary>
        /// <typeparam name="T">目標型別</typeparam>
        /// <param name="sourceList">來源陣列</param>
        /// <returns></returns>
        private static List<T> MapToModelList<T>(this object sourceList) where T : class
        {
            var result = new List<T>();
            foreach (var obj in (IList)sourceList)
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
        private static async Task<List<T>> MapToModelListAsync<T>(this object sourceList) where T : class
        {
            var result = new List<T>();
            foreach (var obj in (IList)sourceList)
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
        private static T MapToModel<T>(this object source) where T : class
        {
            var result = Activator.CreateInstance<T>();
            var sourceType = source.GetType();
            var targetType = typeof(T);
            foreach (var targetProp in targetType.GetProperties())
            {
                if (!targetProp.CanWrite)
                {
                    continue;
                }
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
            }

            return result;
        }

        /// <summary>
        /// 物件與物件轉換(非同步)
        /// </summary>
        /// <typeparam name="T">目標型別</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        private static async Task<T> MapToModelAsync<T>(this object source) where T : class
        {
            return await Task.FromResult(source.MapToModel<T>());
        }
    }
}
