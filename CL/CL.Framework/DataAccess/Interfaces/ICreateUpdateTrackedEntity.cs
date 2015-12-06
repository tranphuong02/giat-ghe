using System;

namespace CL.Framework.DataAccess.Interfaces
{
    public interface ICreateUpdateTrackedEntity
    {
        DateTime CreatedOnUtc { get; set; }
        DateTime UpdatedOnUtc { get; set; }
    }
}
