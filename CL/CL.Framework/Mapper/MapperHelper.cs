using System.Collections.Generic;

namespace CL.Framework.Mapper
{
    public static class MapperHelper
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            AutoMapper.Mapper.CreateMap<TSource, TDestination>().IgnoreAllNonExisting();
            return AutoMapper.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            AutoMapper.Mapper.CreateMap<TSource, TDestination>().IgnoreAllNonExisting();
            return AutoMapper.Mapper.Map(source, destination);
        }

        public static IEnumerable<TDestination> MapTo<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            AutoMapper.Mapper.CreateMap<TSource, TDestination>().IgnoreAllNonExisting();
            return AutoMapper.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
        }

        public static List<TDestination> MapTo<TSource, TDestination>(this List<TSource> source)
        {
            AutoMapper.Mapper.CreateMap<TSource, TDestination>().IgnoreAllNonExisting();
            return AutoMapper.Mapper.Map<List<TSource>, List<TDestination>>(source);
        }
    }
}
