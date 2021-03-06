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
    //get all user keeps
    [HttpGet("user/{userId}")]
    public ActionResult<IEnumerable<Keep>> Get(string userId)
    {
      if (userId == HttpContext.User.Identity.Name)
      {
        return Ok(_repo.getUserKeeps(userId));
      }
      return Ok(_repo.getAnotherUsersKeeps(userId));
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
    public ActionResult<string> Delete(int id)
    {
      if (_repo.DeleteKeep(id))
      {
        return Ok("Successfully deleted!");
      }
      return BadRequest("Unable to delete!");
    }
    [HttpPut("{id}")]
    public ActionResult<Keep> Put(int id, Keep newKeep)
    {
      return _repo.UpdateKeep(newKeep);
    }
  }
}