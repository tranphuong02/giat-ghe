using System.ComponentModel;
using System.Linq;
using AutoMapper;

namespace CL.Framework.Mapper
{
    public static class IgnoreMappingPropertiesExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);

            var existingMaps = AutoMapper.Mapper.GetAllTypeMaps().First(x => x.SourceType == sourceType
                && x.DestinationType == destinationType);
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }

            foreach (var property in sourceType.GetProperties())
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name];

                if (descriptor.PropertyType.Namespace != "Hello.Datasource")
                {
                    continue;
                }

                var destinationProperty = TypeDescriptor.GetProperties(destinationType)[property.Name];
                if (destinationProperty == null)
                {
                    continue;
                }

                expression.ForMember(property.Name, opt => opt.Ignore());
            }
            return expression;
        }
    }
}
