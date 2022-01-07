using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Keep Create(Keep newKeep)
        {
            var sql = @"
            INSERT INTO keeps(name, description, img, creatorId, views, shares, keeps)
            VALUES(@Name, @Description, @Img, @CreatorId, @Views, @Shares, @Keeps)
            SELECT LAST_INSERT_ID(); ";
            var id = _db.ExecuteScalar<int>(sql, newKeep);
            newKeep.Id = id;
            return newKeep;
        }

        internal List<Keep> GetAllKeeps()
        {
            var sql = @"
            SELECT 
                k.*,
                a.* 
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            ;";

            return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
            {
                k.Creator = a;
                return k;
            }).ToList();

        }

        internal Keep GetKeepById(int id)
        {
            var sql = @"
            SELECT
                k.*,
                a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.id = @id
            ;";
            return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
            {
                k.Creator = a;
                return k;
            }).FirstOrDefault();
        }

        internal List<Keep> GetKeepsByUserId(string id)
        {
            var sql = @"
            SELECT *
            FROM keeps
            WHERE creatorId = @id
            ;";
            return _db.Query<Keep>(sql, new { id }).ToList();
        }

        internal void ChangeKeeps(Keep keep)
        {
            var sql = @"
            UPDATE keeps
                SET
                keeps = @Keeps
            WHERE id = @id LIMIT 1
            ;";
            var rowsAffected = _db.Execute(sql, keep);
            if (rowsAffected > 1)
            {

                //alert sysadmin
                throw new Exception("THIS IS REALLY BAD");
            }
            if (rowsAffected < 1)
            {
                throw new Exception("Updated Failed, Likely Bad Id");
            }
        }

        internal Keep Update(Keep keep)
        {
            var sql = @"
            UPDATE keeps
                SET
                name = @Name,
                description = @Description
                img = @Img,
            WHERE id = @id LIMIT 1;";
            var rowsAffected = _db.Execute(sql, keep);
            if (rowsAffected > 1)
            {

                //alert sysadmin
                throw new Exception("THIS IS REALLY BAD");
            }
            if (rowsAffected < 1)
            {
                throw new Exception("Updated Failed, Likely Bad Id");
            }
            return keep;
        }

        internal void ChangeViews(Keep keep)
        {
            var sql = @"
            UPDATE keeps
                SET
                views = @Views
            WHERE id = @id LIMIT 1
            ;";
            var rowsAffected = _db.Execute(sql, keep);
            if (rowsAffected > 1)
            {

                //alert sysadmin
                throw new Exception("THIS IS REALLY BAD");
            }
            if (rowsAffected < 1)
            {
                throw new Exception("Updated Failed, Likely Bad Id");
            }
        }

        internal void Delete(int keepId)
        {
            var sql = @"
            DELETE FROM keeps WHERE id = @keepId
            ;";
            _db.Execute(sql, new { keepId });
        }
    }
}