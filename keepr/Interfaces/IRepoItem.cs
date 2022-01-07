using System;

namespace keepr.Interfaces
{
    public interface IRepoItem
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}