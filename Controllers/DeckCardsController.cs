using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using keepr.models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/deckcards")]
  [ApiController]

  public class DeckCardsController : ControllerBase
  {
    private readonly DeckCardsRepository _repo;
    public DeckCardsController(DeckCardsRepository repo)
    {
      _repo = repo;
    }

    // GET cards by deckid
    [HttpGet("{id}")]
    public ActionResult<Card> Get(int id)
    {
      return Ok(_repo.GetCardsByDeckId(id));
    }

    // POST api/values
    [HttpPost]
    public ActionResult<DeckCard> Post([FromBody] DeckCard value)
    {
      DeckCard result = _repo.addDeckCard(value);
      return Created("api/library/" + result.Id, result);
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] DeckCard value)
    {

    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(DeckCard dc)
    {
      if (_repo.DeleteDeckCard(dc))
      {
        return Ok("Successfully Deleted!");
      }
      return BadRequest("Unable to delete!");
    }
  }
}