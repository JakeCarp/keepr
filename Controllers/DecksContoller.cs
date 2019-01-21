using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using keepr.models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DecksController : ControllerBase
  {
    private readonly DecksRepository _repo;
    public DecksController(DecksRepository repo)
    {
      _repo = repo;
    }

    //GET user decks
    [HttpGet("{userId}")]
    public ActionResult<IEnumerable<Deck>> Get(string userId)
    {
      return Ok(_repo.getUserDecks(userId));
    }
    //Get all decks
    [HttpGet]
    public ActionResult<IEnumerable<Deck>> Get()
    {
      return Ok(_repo.getAll());
    }
    //add a new Deck
    [HttpPost]
    public ActionResult<Deck> Post([FromBody] Deck value)
    {
      Deck result = _repo.addDeck(value);
      return Created("/decks/" + result.Id, result);
    }
    //Delete a Deck
    [HttpDelete]
    public ActionResult<string> Delete(int id)
    {
      if (_repo.DeleteDeck(id))
      {
        return Ok("Successfully Deleted");
      }
      return BadRequest("Unable to Delete");
    }
  }
}