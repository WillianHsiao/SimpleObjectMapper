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
        /// <typeparam name="TTarget">目標型別</typeparam>
        /// <param name="source">來源物件</param>
        /// <returns></returns>
        public static TTarget ToModel<TTarget>(this object source) where TTarget : class
        {
            return source.Map<TTarget>();
        }
    }
}
