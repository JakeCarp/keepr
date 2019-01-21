using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.models;

namespace keepr.Repositories
{
  public class CardsRepository
  {
    private readonly IDbConnection _db;
    public CardsRepository(IDbConnection db)
    {
      _db = db;
    }
    //get all cards
    public IEnumerable<Card> GetAll()
    {
      return _db.Query<Card>("SELECT * FROM Cards");
    }
    //get a card by Id
    public Card GetById(int id)
    {
      return _db.QueryFirstOrDefault<Card>("SELECT * FROM Cards WHERE id = @id", new { id });
    }
  }
}