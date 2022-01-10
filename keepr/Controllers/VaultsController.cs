using System;
using System.Collections.Generic;
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
        private readonly VaultKeepsService _vks;


        public VaultsController(VaultsService vs, VaultKeepsService vks)
        {
            _vs = vs;
            _vks = vks;
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

        public async Task<ActionResult<Vault>> GetVaultById(int id)
        {
            try
            {
                var userInfo = await HttpContext.GetUserInfoAsync<Account>();
                if (userInfo == null)
                {
                    Vault vaultNA = _vs.GetById(id);
                    return vaultNA;
                }
                else
                {
                    Vault vault = _vs.GetById(id, userInfo.Id);
                    return Ok(vault);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        private Vault GetVaultByIdNoAuth(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}/keeps")]

        public ActionResult<List<KeepViewModel>> GetKeepsByVaultId(int id)
        {
            try
            {
                List<KeepViewModel> keeps = _vks.GetKeepsByVaultId(id);
                return Ok(keeps);
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
                update.Id = id;
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