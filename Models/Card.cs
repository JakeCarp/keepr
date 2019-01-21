using System.Collections.Generic;

namespace keepr.models
{
  public class Card
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public string ImgUrl { get; set; }
    public int Uses { get; set; }
  }
}