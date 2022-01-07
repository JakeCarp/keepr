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
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;

        public KeepsController(KeepsService ks)
        {
            _ks = ks;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
        {
            try
            {
                var userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newKeep.CreatorId = userInfo.Id;
                Keep keep = _ks.Create(newKeep);
                keep.Creator = userInfo;
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [HttpGet]
        public ActionResult<List<Keep>> GetAllKeeps()
        {
            try
            {
                List<Keep> keeps = _ks.GetAllKeeps();
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]

        public ActionResult<Keep> GetKeepById(int id)
        {
            try
            {
                Keep keep = _ks.GetKeepById(id);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult<Keep>> UpdateKeep([FromBody] Keep update, int id)
        {
            try
            {
                update.Id = id;
                var userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Keep keep = _ks.UpdateKeep(userInfo.Id, update);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult<String>> DeleteKeep(int id)
        {
            try
            {
                var userInfo = await HttpContext.GetUserInfoAsync<Account>();
                String res = _ks.DeleteKeep(userInfo.Id, id);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}