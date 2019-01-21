namespace keepr.models
{
  public class DeckCard
  {
    public int Id { get; set; }
    public int DeckId { get; set; }
    public int CardId { get; set; }
    public string UserId { get; set; }
  }
}