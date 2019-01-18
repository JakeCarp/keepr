using System.Collections.Generic;

namespace keepr.models
{
  public class Card
  {
    public string Id { get; set; }
    public int MultiverseId { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public string ImgUrl { get; set; }
    public int Uses { get; set; }
    //use if implementing search functionality
    // public List<string> Names { get; set; }
    // public string ManaCost { get; set; }
    // public int CMC { get; set; }
    // public List<string> Colors { get; set; }
    // public List<string> ColorIdentity { get; set; }
    // public string Type { get; set; }
    // public string Subtypes { get; set; }
    // public string Rarity { get; set; }
    // public string Set { get; set; }
    // public string Artist { get; set; }
    // public string Power { get; set; }
    // public string Toughness { get; set; }
    // public string Layout { get; set; }
    // public string Legalities { get; set; }
    // public string GameFormat { get; set; }
  }
}