using System.Collections.Generic;

namespace keepr.models
{
  public class Deck
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string GameFormat { get; set; }
    public List<string> Colors { get; set; }
    public bool Private { get; set; }
    public string UserId { get; set; }

    public int saves { get; set; }
    public int shares { get; set; }
  }
}