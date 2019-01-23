using System.Collections.Generic;

namespace keepr.models
{
  public class Vault
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public bool IsPrivate { get; set; }
    public string ImgUrl { get; set; }
    public string UserId { get; set; }
  }
}