using CL.Framework.Autofac.Interfaces;
using System;

namespace CL.Framework.DataAccess.Interfaces
{
    public interface IHierarchyEntity : IBaseEntity, IDependency
    {
        int? ParentId { get; set; }
        string Ancestors { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class ParentPropertyAttribute : Attribute
    {
        // This is a positional argument
        public ParentPropertyAttribute(string property)
        {
            Property = property;
        }

        public string Property { get; set; }
    }
}
