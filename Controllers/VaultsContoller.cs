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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsRepository _repo;
    public VaultsController(VaultsRepository repo)
    {
      _repo = repo;
    }

    //GET user vaults
    [HttpGet("{userId}")]
    public ActionResult<IEnumerable<Vault>> Get(string userId)
    {
      if (userId == HttpContext.User.Identity.Name)
      {
        return Ok(_repo.getUserVaults(userId));
      }
      return Ok(_repo.getAnotherUsersVaults(userId));
    }
    //add a new vault
    [HttpPost]
    public ActionResult<Vault> Post([FromBody] Vault value)
    {
      value.UserId = HttpContext.User.Identity.Name;
      if (value.UserId != null)
      {
        Vault result = _repo.addVault(value);
        return Created("/Vaults/" + result.Id, result);
      }
      return BadRequest("Unauthorized");
    }
    //Delete a vault
    [HttpDelete]
    public ActionResult<string> Delete(Vault value)
    {
      if (value.UserId != HttpContext.User.Identity.Name)
      {
        return BadRequest("unauthorized");
      }
      if (_repo.DeleteVault(value))
      {
        return Ok("Successfully Deleted");
      }
      return BadRequest("Unable to Delete");
    }
  }
}