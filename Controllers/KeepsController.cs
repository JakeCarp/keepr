using Microsoft.AspNetCore.Mvc;
using keepr.Repositories;
using System.Collections.Generic;
using keepr.models;

namespace keepr.controllers
{
  [Route("api/cards")]
  [ApiController]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsRepository _repo;

    public KeepsController(KeepsRepository repo)
    {
      _repo = repo;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      return Ok(_repo.GetAll());
    }
    [HttpGet("{id}")]
    public ActionResult<Keep> Get(int id)
    {
      return Ok(_repo.GetById(id));
    }
  }
}