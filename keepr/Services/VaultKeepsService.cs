using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsService _vs;

        private readonly KeepsService _ks;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsService vs, KeepsService ks)
        {
            _repo = repo;
            _vs = vs;
            _ks = ks;
        }

        internal VaultKeep Create(VaultKeep newVaultKeep)
        {
            Vault vault = _vs.GetById(newVaultKeep.VaultId, newVaultKeep.CreatorId);
            if (vault.CreatorId != newVaultKeep.CreatorId)
            {
                throw new Exception("You Cannot Add Keeps To a Vault You Do not own");
            }
            return _repo.Create(newVaultKeep);
        }

        internal VaultKeep Delete(int id, string userId)
        {
            var vaultKeep = isVaultKeepOwner(id, userId);
            _repo.Delete(id);
            _ks.decrementKeeps(vaultKeep.KeepId);
            return vaultKeep;
        }

        internal List<KeepViewModel> GetKeepsByVaultId(int id)
        {
            Vault vault = _vs.GetById(id);
            if (vault.IsPrivate)
            {
                throw new Exception("This Vault Is Private!");
            }

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