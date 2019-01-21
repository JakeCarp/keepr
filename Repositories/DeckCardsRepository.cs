using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.models;

namespace keepr.Repositories
{
  public class DeckCardsRepository
  {
    private readonly IDbConnection _db;
    public DeckCardsRepository(IDbConnection db)
    {
      _db = db;
    }

    //Get Cards By DeckId
    public IEnumerable<Card> GetCardsByDeckId(int id)
    {
      return _db.Query<Card>($@"
      SELECT * FROM deckcards dc
      INNER JOIN card c ON c.id = dc.cardId
      WHERE (deckId = @id);
      ", new { id });
    }
    //Get Decksby Card Id
    public IEnumerable<Deck> GetDecksByCardId(int id)
    {
      return _db.Query<Deck>($@"
      SELECT * FROM deckcards dc
      INNER JOIN deck d on d.id = dc.deckId
      WHERE (cardId = @id);
      ", new { id });
    }

    //addDeckCard
    public DeckCard addDeckCard(DeckCard dc)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO deckcards(cardId, deckId)
      VALUES(@cardId, @deckId);
      SELECT LAST_INSERT_ID();
      ", dc);
      dc.Id = id;
      return dc;
    }

    //deleteDeckCard
    public bool DeleteDeckCard(DeckCard dc)
    {
      int success = _db.Execute(@"DELETE FROM deckcards WHERE cardId = @cardId AND deckId = @deckId", dc);
      return success != 0;
    }
  }
}