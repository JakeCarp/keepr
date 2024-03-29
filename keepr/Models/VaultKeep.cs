using System;
using keepr.Interfaces;

namespace keepr.Models
{
    public class VaultKeep : IRepoItem
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public int VaultId { get; set; }
        public int KeepId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class VaultKeepViewModel : VaultKeep
    {
        public Account Creator { get; set; }
        public Keep Keep { get; set; }
        public Vault Vault { get; set; }
    }
}