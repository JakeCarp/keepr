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
      return Ok(_repo.getUserVaults(userId));
    }
    //add a new vault
    [HttpPost]
    public ActionResult<Vault> Post([FromBody] Vault value)
    {
      Vault result = _repo.addVault(value);
      return Created("/Vaults/" + result.Id, result);
    }
    //Delete a vault
    [HttpDelete]
    public ActionResult<string> Delete(int id)
    {
      if (_repo.DeleteVault(id))
      {
        return Ok("Successfully Deleted");
      }
      return BadRequest("Unable to Delete");
    }
  }
}