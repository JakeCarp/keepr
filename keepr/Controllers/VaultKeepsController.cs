using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vks;
        private readonly KeepsService _ks;

        public VaultKeepsController(VaultKeepsService vks, KeepsService ks)
        {
            _vks = vks;
            _ks = ks;
        }


        [HttpPost]
        [Authorize]

        public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep newVaultKeep)
        {
            try
            {
                var userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newVaultKeep.CreatorId = userInfo?.Id;
                VaultKeep vk = _vks.Create(newVaultKeep);
                _ks.incrementKeeps(vk.KeepId);
                return Ok(vk);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<String>> DeleteVaultKeeps(int id)
        {
            try
            {
                var userInfo = await HttpContext.GetUserInfoAsync<Account>();
                var vk = _vks.Delete(id, userInfo.Id);
                _ks.decrementKeeps(vk.KeepId);
                return Ok("Delete Successful");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}