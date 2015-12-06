using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CL.Framework.DataAccess
{
    public static class ReflectionUtilities
    {
        /// <summary>
        ///     Get attribute of member
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this MemberInfo memberInfo) where T : class
        {
            var customAttributes = memberInfo.GetCustomAttributes(typeof(T), false);
            var attribute = customAttributes.FirstOrDefault(a => a is T) as T;
            return attribute;
        }

        /// <summary>
        ///     Attempts to set a named property of an entity to an arbitrary value. The value is set if the property is found.
        /// </summary>
        /// <typeparam name="T">An entity deriving of type EntityObject.</typeparam>
        /// <param name="entityToSet">The instance of the entity whose value will be set.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="value">The value of the property to set.</param>
        public static void SetProperty<T>(this T entityToSet, string propertyName, object value)
        {
            var targetProperty = entityToSet.GetType().GetProperty(propertyName);
            if (targetProperty != null)
                targetProperty.SetValue(entityToSet, value, null);
        }

        /// <summary>
        ///     Get all properties from type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<PropertyInfo> GetAllPropertiesOfType(Type type)
        {
            return type.GetProperties().ToList();
        }

        /// <summary>
        ///     Get all properties from type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<string> GetAllPropertyNamesOfType(Type type)
        {
            var properties = type.GetProperties();
            return properties.Select(p => p.Name).ToList();
        }

        /// <summary>
        ///     Get value of property by name
        /// </summary>
        /// <param name="entity">the entity</param>
        /// <param name="propertyName">the property name</param>
        /// <returns></returns>
        public static object GetPropertyValue<T>(this T entity, string propertyName)
        {
            var targetProperty = entity.GetType().GetProperty(propertyName);
            if (targetProperty != null)
                return targetProperty.GetValue(entity);
            return null;
        }
    }
}
