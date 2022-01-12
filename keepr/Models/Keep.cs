using System;
using keepr.Interfaces;

namespace keepr.Models
{
    public class Keep : IRepoItem
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public int Views { get; set; }
        public int Shares { get; set; }
        public int Keeps { get; set; }

        public string Description { get; set; }

        public Account Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class KeepViewModel : Keep
    {
        public int vaultKeepId { get; set; }

    }
}