using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.models;

namespace keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    //get all keeps
    public IEnumerable<Keep> GetAll()
    {
      return _db.Query<Keep>("SELECT * FROM Keeps WHERE isPrivate = 0");
    }
    //get user keeps
    public IEnumerable<Keep> getUserKeeps(string userId)
    {
      return _db.Query<Keep>($"SELECT * FROM Keeps WHERE userId=@userId", new { userId });
    }
    //get another users keeps
    public IEnumerable<Keep> getAnotherUsersKeeps(string userId)
    {
      return _db.Query<Keep>($"SELECT * FROM Keeps WHERE userId = @userId AND isPrivate = 0");
    }
    //get a keep by Id
    public Keep GetById(int id)
    {
      return _db.QueryFirstOrDefault<Keep>("SELECT * FROM Keeps WHERE id = @id", new { id });
    }

    //add keep
    public Keep AddKeep(Keep newKeep)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO keeps(name, text, imgUrl, userId, views, shares, keeps, isPrivate, creatorName)
      VALUES( @name, @text, @imgUrl, @userId, @views, @shares, @keeps, @isPrivate, @creatorName);
      SELECT LAST_INSERT_ID();
      ", newKeep);
      newKeep.Id = id;
      return newKeep;
    }

    //deleteKeep
    public bool DeleteKeep(int id)
    {
      int success = _db.Execute(@"DELETE FROM keeps WHERE id = @id", new { id });
      return success != 0;
    }
    //Update Keep
    public Keep UpdateKeep(Keep value)
    {
      _db.Execute(@"
      UPDATE keeps 
      SET views = @views, shares = @shares, keeps = @keeps, isPrivate = @isPrivate
      WHERE id = @id AND userId = @userId;
      ", value);
      return value;
    }
  }
}