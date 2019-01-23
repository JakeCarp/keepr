using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.models;

namespace keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }
    //Get all user decks
    public IEnumerable<Vault> getUserVaults(string userId)
    {
      return _db.Query<Vault>($"SELECT * FROM Vaults WHERE userId=@userId", new { userId });
    }
    public IEnumerable<Vault> getAnotherUsersVaults(string userId)
    {
      return _db.Query<Vault>($"SELECT * FROM Vaults WHERE userId = @userId AND isprivate = 0", new { userId });
    }
    //Get a Deck by Id
    public Vault GetById(int id)
    {
      return _db.QueryFirstOrDefault<Vault>($"SELECT * FROM Vaults WHERE id = @id", new { id });
    }
    //addDeck
    public Vault addVault(Vault newVault)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO Vaults(name, description,  isprivate, userId, imgUrl)
      VALUES(@name, @description, @isprivate, @userId, @imgUrl);
      SELECT LAST_INSERT_ID();", newVault);
      newVault.Id = id;
      return newVault;
    }

    public bool DeleteVault(Vault value)
    {
      int success = _db.Execute("DELETE FROM Vaults WHERE id = @value.Id", value);
      return success != 0;
    }




  }
}