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
    //get all cards
    public IEnumerable<Keep> GetAll()
    {
      return _db.Query<Keep>("SELECT * FROM Keeps");
    }
    //get a card by Id
    public Keep GetById(int id)
    {
      return _db.QueryFirstOrDefault<Keep>("SELECT * FROM Keeps WHERE id = @id", new { id });
    }
  }
}