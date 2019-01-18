using System.Collections.Generic;
using keepr.models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.controllers
{
  [Route("Decks")]
  [ApiController]
  public class DecksController : ControllerBase
  {
    private readonly DecksRepository _repo;
    public DecksController(DecksRepository repo)
    {
      _repo = repo;
    }

    //GET /values
    [HttpGet]
    public ActionResult<IEnumerable<Deck>> Get(string userId)
    {
      return Ok(_repo.getUserDecks(userId));
    }









  }
}