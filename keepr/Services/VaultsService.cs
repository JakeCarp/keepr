using System;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class VaultsService
    {

        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        internal Vault Create(Vault newVault)
        {
            return _repo.Create(newVault);
        }

        internal Vault GetById(int id)
        {
            Vault vault = _repo.GetById(id);
            if (vault == null)
            {
                throw new Exception("Bad Vault Id");
            }
            return vault;
        }

        internal object Update(Vault update, string id)
        {
            var vault = isVaultOwner(id, update.Id);
            vault.Name = update.Name ?? vault.Name;
            vault.Description = update.Description ?? vault.Description;
            vault.IsPrivate = update.IsPrivate;
            return _repo.Update(vault);

        }

        internal object Delete(int vaultId, string userId)
        {
            Vault vault = isVaultOwner(userId, vaultId);
            _repo.Delete(vaultId);
            return "Delete Successful";
        }

        internal Vault isVaultOwner(string userId, int vaultId)
        {
            Vault vault = GetById(vaultId);
            if (vault.CreatorId != userId)
            {
                throw new Exception("This is not Your Vault");
            }
            return vault;
        }
    }
}