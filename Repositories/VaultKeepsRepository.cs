using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.models;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    //Get All user VaultKeeps
    public IEnumerable<VaultKeep> GetUserVaultKeeps(int vaultId)
    {
      return _db.Query<VaultKeep>($"SELECT * FROM VaultKeeps WHERE vaultId = @vaultId", new { vaultId });
    }
    //Get keeps By VaultId
    public IEnumerable<Keep> GetKeepsByVaultId(int id)
    {
      return _db.Query<Keep>($@"
      SELECT * FROM VaultKeeps vk
      INNER JOIN keeps k ON k.id = vk.keepId
      WHERE (vaultId = @id);
      ", new { id });
    }

    //addVaultKeeps
    public VaultKeep addVaultKeep(VaultKeep vk)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO vaultkeeps(keepId, vaultId, userId)
      VALUES(@KeepId, @VaultId, @UserId);
      SELECT LAST_INSERT_ID();
      ", vk);
      vk.Id = id;
      return vk;
    }

    //deleteDeckCard
    public bool DeleteVaultKeep(int keepId, int vaultId)
    {
      int success = _db.Execute(@"DELETE FROM vaultkeeps WHERE keepId = @keepId AND vaultId = @vaultId", new { keepId, vaultId });
      return success != 0;
    }
  }
}