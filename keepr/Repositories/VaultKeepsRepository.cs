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
            VALUES(@CreatorId, @VaultId, @KeepId)
            SELECT LAST_INSERT_ID()
            ;";
            var id = _db.ExecuteScalar<int>(sql);
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

            return _db.QueryFirstOrDefault<VaultKeep>(sql);
        }

        internal void Delete(int id)
        {
            var sql = @"
            DELETE FROM vaultKeeps
            WHERE id = @id
            ;";
            _db.Execute(sql, new { id });
        }

        internal List<VaultKeepViewModel> GetKeepsByVaultId(int id)
        {
            var sql = @"
            SELECT
                vk.*,
                a.*,
                k.*
            FROM vaultKeeps vk
            JOIN accounts a ON a.id = vk.creatorId
            JOIN keeps k ON k.id = vk.keepId
            WHERE vk.vaultId = @id
            ;";
            return _db.Query<VaultKeepViewModel, Account, Keep, VaultKeepViewModel>(sql, (vaultKeep, acct, keep) =>
            {
                vaultKeep.Creator = acct;
                vaultKeep.Keep = keep;
                return vaultKeep;
            }, new { id }).ToList();
        }
    }
}