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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;

        public VaultsController(VaultsService vs)
        {
            _vs = vs;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault newVault)
        {
            try
            {
                var userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newVault.CreatorId = userInfo.Id;
                Vault vault = _vs.Create(newVault);
                vault.Creator = userInfo;
                return Ok(vault);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]

        public ActionResult<Vault> GetVaultById(int id)
        {
            try
            {
                var vault = _vs.GetById(id);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault update, int id)
        {
            try
            {
                var userInfo = await HttpContext.GetUserInfoAsync<Account>();
                var vault = _vs.Update(update, userInfo.Id);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult<String>> DeleteVault(int id)
        {
            try
            {
                var userInfo = await HttpContext.GetUserInfoAsync<Account>();
                var res = _vs.Delete(id, userInfo.Id);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}