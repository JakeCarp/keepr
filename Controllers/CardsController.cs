using Microsoft.AspNetCore.Mvc;
using keepr.Repositories;
using System.Collections.Generic;
using keepr.models;

namespace keepr.controllers
{
  [Route("api/cards")]
  [ApiController]
  public class CardsController : ControllerBase
  {
    private readonly CardsRepository _repo;

    public CardsController(CardsRepository repo)
    {
      _repo = repo;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Card>> Get()
    {
      return Ok(_repo.GetAll());
    }
    [HttpGet("{id}")]
    public ActionResult<Card> Get(int id)
    {
      return Ok(_repo.GetById(id));
    }
  }
}