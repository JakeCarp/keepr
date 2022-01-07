using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault Create(Vault newVault)
        {
            var sql = @"
            INSERT INTO vaults(name, description, isPrivate)
            VALUES(@Name, @Description, @IsPrivate)
            SELECT LAST_INSERT_ID();";
            var id = _db.ExecuteScalar<int>(sql);
            return newVault;
        }

        internal Vault GetById(int id)
        {
            var sql = @"
            SELECT 
                v.*,
                a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.id = @id
            ;";
            return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
            {
                v.Creator = a;
                return v;
            }).FirstOrDefault();
        }

        internal object Update(Vault vault)
        {

            var sql = @"
            UPDATE vaults
                SET
                    name = @Name,
                    description = @Description,
                    isPrivate = @IsPrivate,
            WHERE id = @id LIMIT 1;";
            var rowsAffected = _db.Execute(sql, vault);
            if (rowsAffected > 1)
            {
                //alert sysadmin
                throw new Exception("THE BIG BAD, ALTER SYSADMIN");
            }
            if (rowsAffected < 1)
            {
                throw new Exception("Update Failed, Likely Bad Id");
            }
            return vault;
        }

        internal List<Vault> GetUserVaults(string queryid)
        {
            var sql = @"
            SELECT
            *   
            FROM vaults
            WHERE v.creatorId = @queryid
            ;";
            return _db.Query<Vault>(sql, new { queryid }).ToList();
        }

        internal void Delete(int vaultId)
        {
            var sql = @"
            DELETE FROM vaults WHERE id = @vaultId
            ;";
            _db.Execute(sql, new { vaultId });

        }
    }
}