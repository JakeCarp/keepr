using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;

        public VaultKeepsService(VaultKeepsRepository repo)
        {
            _repo = repo;
        }

        internal VaultKeep Create(VaultKeep newVaultKeep)
        {
            return _repo.Create(newVaultKeep);
        }

        internal VaultKeep Delete(int id, string userId)
        {
            var vaultKeep = isVaultKeepOwner(id, userId);
            _repo.Delete(id);
            return vaultKeep;
        }

        internal List<VaultKeepViewModel> GetKeepsByVaultId(int id)
        {
            return _repo.GetKeepsByVaultId(id);
        }

        internal VaultKeep isVaultKeepOwner(int vaultKeepId, string userId)
        {
            VaultKeep vk = getVaultKeepById(vaultKeepId);
            if (vk.CreatorId != userId)
            {
                throw new Exception("This is not your VaultKeep!");
            }
            return vk;
        }

        internal VaultKeep getVaultKeepById(int id)
        {
            VaultKeep vk = _repo.GetById(id);
            if (vk == null)
            {
                throw new Exception("Bad VaultKeep Id");
            }
            return vk;
        }
    }
}