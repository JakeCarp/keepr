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

    //Get Cards By DeckId
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
    public bool DeleteVaultKeep(VaultKeep vk)
    {
      int success = _db.Execute(@"DELETE FROM vaultkeeps WHERE keepId = @cardId AND vaultId = @deckId", vk);
      return success != 0;
    }
  }
}