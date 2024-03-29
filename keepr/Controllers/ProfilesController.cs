using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly AccountService _as;
        private readonly KeepsService _ks;

        private readonly VaultsService _vs;

        public ProfilesController(AccountService @as, KeepsService ks, VaultsService vs)
        {
            _as = @as;
            _ks = ks;
            _vs = vs;
        }

        [HttpGet("{id}")]
        public ActionResult<Account> GetProfile(string id)
        {
            try
            {
                string email = _as.GetProfileEmailById(id);
                Account acct = _as.GetProfileByEmail(email);
                return Ok(acct);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]

        public ActionResult<List<Keep>> GetUserKeeps(string id)
        {
            try
            {
                List<Keep> keeps = _ks.GetKeepsByUserId(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        [HttpGet("{id}/vaults")]
        public async Task<ActionResult<List<Vault>>> GetUserVaults(string id)
        {
            try
            {
                var userInfo = await HttpContext.GetUserInfoAsync<Account>();
                if (userInfo == null)
                {
                    List<Vault> vaults = _vs.GetUserVaults(id);
                    return Ok(vaults);
                }
                else
                {
                    List<Vault> vaults = _vs.GetUserVaults(id, userInfo.Id);
                    return Ok(vaults);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}