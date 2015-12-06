namespace CL.Framework.Autofac.Interfaces
{
    /// <summary>
    /// Inherit from this interface to register auto fact dependency as per dependency
    /// </summary>
    public interface IDependency
    {
    }

    /// <summary>
    /// Inherit from this interface to register auto fact dependency as per request
    /// </summary>
    public interface IPerRequestDependency : IDependency
    {
    }

    /// <summary>
    /// Inherit from this interface to register auto fact dependency as a singleton
    /// </summary>
    public interface ISingletonDependency : IDependency
    {
    }
}
