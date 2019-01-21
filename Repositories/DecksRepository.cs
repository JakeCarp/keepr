using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.models;

namespace keepr.Repositories
{
  public class DecksRepository
  {
    private readonly IDbConnection _db;
    public DecksRepository(IDbConnection db)
    {
      _db = db;
    }
    //get all decks
    public IEnumerable<Deck> getAll()
    {
      return _db.Query<Deck>("SELECT * FROM Decks");
    }
    //Get all user decks
    public IEnumerable<Deck> getUserDecks(string userId)
    {
      return _db.Query<Deck>($"SELECT * FROM Decks WHERE userId=@userId", new { userId });
    }

    //Get a Deck by Id
    public Deck GetById(int id)
    {
      return _db.QueryFirstOrDefault<Deck>($"SELECT * FROM Decks WHERE id = @id", new { id });
    }
    //addDeck
    public Deck addDeck(Deck newDeck)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO Decks(name, description, colors, private, userId, saves, shares)
      VALUES(@name, @description, @colors, @private, @userId, @saves, @shares);
      SELECT LAST_INSERT_ID();", newDeck);
      newDeck.Id = id;
      return newDeck;
    }

    public bool DeleteDeck(int id)
    {
      int success = _db.Execute("DELETE FROM Decks WHERE id = @id", new { id });
      return success != 0;
    }




  }
}