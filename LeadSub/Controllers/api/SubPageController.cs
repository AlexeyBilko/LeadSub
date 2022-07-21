using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LeadSub.Controllers.api
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubPageController : Controller
    {
        SubPagesService subPagesService;
        public SubPageController(SubPagesService subPagesService)
        {
            this.subPagesService = subPagesService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<SubPageDTO>>> Get(string userId)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string? currentUserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            if (currentUserId == userId)
            {
                return Ok(await subPagesService.GetMyAsync(userId));
            }
            else return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<SubPageDTO>> Post(SubPageDTO subpage)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string? currentUserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            if (currentUserId == subpage.UserId)
            {
                if (subpage != null)
                {
                    await subPagesService.AddAsync(subpage);
                    return Ok(subpage);
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SubPageDTO>> Delete(int id)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string? currentUserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            SubPageDTO subpage = await subPagesService.GetAsync(id);

            if (currentUserId == subpage.UserId)
            {
                if (subpage != null)
                {
                    await subPagesService.DeleteAsync(id);
                    return Ok(subpage);
                }
            }
            return NotFound();
        }
        [HttpPut]
        public async Task<ActionResult<SubPageDTO>> Put(SubPageDTO subpage)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string? currentUserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            if (subpage == null || currentUserId == subpage.UserId)
            {
                return BadRequest();
            }
            if (subPagesService.Get(subpage.Id) == null)
            {
                return NotFound();
            }
            await subPagesService.UpdateAsync(subpage);
            return Ok(subpage);
        }
    }
}
