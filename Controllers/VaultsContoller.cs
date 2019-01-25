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
    //Get Vault By Id 
    [HttpGet("{Id}")]
    public ActionResult<Vault> Get(int Id)
    {
      return Ok(_repo.GetById(Id));
    }
    //GET user vaults
    [HttpGet("user/{userId}")]
    public ActionResult<IEnumerable<Vault>> Get(string userId)
    {
      if (userId == HttpContext.User.Identity.Name)
      {
        return Ok(_repo.getUserVaults(userId));
      }
      return Ok(_repo.getAnotherUsersVaults(userId));
    }
    //update vault privacy
    [HttpPut("{Id}")]
    public ActionResult<Vault> put([FromBody] Vault value)
    {
      return _repo.UpdateVault(value);
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
    [HttpDelete("{id}")]
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