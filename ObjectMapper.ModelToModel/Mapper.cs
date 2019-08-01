using System;
using System.Linq;
using ObjectMapper.Common.Attribute;
using ObjectMapper.Common.Exception;

namespace ObjectMapper.ModelToModel
{
    public static class Mapper
    {
        /// <summary>
        /// 執行物件轉換
        /// </summary>
        /// <typeparam name="TTarget">目標型別</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TTarget Map<TTarget>(this object source) where TTarget : class
        {
            var result = Activator.CreateInstance<TTarget>();
            var sourceType = source.GetType();
            var targetType = typeof(TTarget);
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
    }
}
