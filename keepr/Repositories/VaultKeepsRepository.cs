using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Create(VaultKeep newVaultKeep)
        {
            var sql = @"
            INSERT INTO vaultKeeps(creatorId, vaultId, keepId)
            VALUES(@CreatorId, @VaultId, @KeepId);
            SELECT LAST_INSERT_ID()
            ;";
            var id = _db.ExecuteScalar<int>(sql, newVaultKeep);
            newVaultKeep.Id = id;
            return newVaultKeep;

        }

        internal VaultKeep GetById(int id)
        {
            var sql = @"
            SELECT
                *
            FROM vaultKeeps vk
            WHERE vk.id = @id
            ;";

            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }

        internal void Delete(int vaultKeepId)
        {
            var sql = @"
            DELETE FROM vaultKeeps
            WHERE id = @vaultKeepId
            ;";
            _db.Execute(sql, new { vaultKeepId });
        }

        internal List<KeepViewModel> GetKeepsByVaultId(int id)
        {

            //TODO This is not good
            var sql = @"
            SELECT
            vk.*  ,
                k.*,
                a.*
            FROM vaultKeeps vk
            JOIN keeps k ON k.id = vk.keepId
            JOIN accounts a ON k.creatorId = a.id
            WHERE vk.vaultId = @id 
            ;";
            return _db.Query<KeepViewModel, Keep, Account, KeepViewModel>(sql, (kv, k, acct) =>
            {
                kv.Name = k.Name;
                kv.Img = k.Img;
                kv.Views = k.Views;
                kv.Shares = k.Shares;
                kv.Keeps = k.Keeps;
                kv.Description = k.Description;
                kv.CreatedAt = k.CreatedAt;
                kv.UpdatedAt = k.UpdatedAt;
                kv.Creator = acct;
                kv.vaultKeepId = id;
                return kv;
            }, new { id }).ToList();
        }
    }
}