using System.Collections.Generic;

namespace keepr.models
{
  public class Keep
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public string UserId { get; set; }
    public string ImgUrl { get; set; }
    public int Views { get; set; }
    public int Shares { get; set; }
    public int Keeps { get; set; }
  }
}