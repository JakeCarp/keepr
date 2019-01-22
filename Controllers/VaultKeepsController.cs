using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using keepr.models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]

  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsController(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    // GET Keeps by VaultId
    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Keep>> Get(int id)
    {
      return Ok(_repo.GetKeepsByVaultId(id));
    }

    // POST api/values
    [HttpPost]
    public ActionResult<VaultKeep> Post([FromBody] VaultKeep value)
    {
      value.UserId = HttpContext.User.Identity.Name;
      if (value.UserId != null)
      {
        VaultKeep result = _repo.addVaultKeep(value);
        return Created("api/VaultKeeps/" + result.Id, result);
      }
      return BadRequest();
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(VaultKeep vk)
    {
      if (_repo.DeleteVaultKeep(vk))
      {
        return Ok("Successfully Deleted!");
      }
      return BadRequest("Unable to delete!");
    }
  }
}