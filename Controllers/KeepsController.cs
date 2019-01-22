using Microsoft.AspNetCore.Mvc;
using keepr.Repositories;
using System.Collections.Generic;
using keepr.models;

namespace keepr.controllers
{
  [Route("api/[controller]")]
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
      Keep result = _repo.GetById(id);
      if (result != null)
      {
        return Ok(result);
      }
      return BadRequest();
    }
    [HttpPost]
    public ActionResult<Keep> Post([FromBody] Keep value)
    {
      value.UserId = HttpContext.User.Identity.Name;
      if (value.UserId != null)
      {
        Keep result = _repo.AddKeep(value);
        return Created("/api/Keep/" + result.Id, result);
      }
      return BadRequest("Unauthorized");
    }
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(Keep value)
    {
      if (value.UserId != HttpContext.User.Identity.Name)
      {
        return BadRequest("Cannot Delete Keeps That Don't Belong To You!");
      }
      if (_repo.DeleteKeep(value.Id))
      {
        return Ok("Successfully deleted!");
      }
      return BadRequest("Unable to delete!");
    }
    [HttpPut]
    public ActionResult<string> Put(int id, [FromBody] Keep value)
    {
      if (value.UserId != HttpContext.User.Identity.Name)
      {
        return BadRequest("Unauthorized");
      }
      if (_repo.UpdateKeep(id, value))
      {
        return Ok("Successfully Updated");
      }
      return BadRequest("Unable to Update");
    }
  }
}