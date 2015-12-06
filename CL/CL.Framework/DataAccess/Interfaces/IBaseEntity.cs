using System;

namespace CL.Framework.DataAccess.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        bool IsPublish { get; set; }

        bool IsDelete { get; set; }

        DateTime CreatedDate { get; set; }

        DateTime UpdatedDate { get; set; }
    }
}
