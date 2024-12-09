using Infrastructure.SQLServer;
using Microsoft.AspNetCore.Http;
using Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Domain.Models.DAO;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileSharesController : ControllerBase
{
    private readonly SqlDBContext _context;
    private readonly IUserClaimsService _userClaimsService;

    public ProfileSharesController(SqlDBContext context, IUserClaimsService userClaimsService)
    {
        _context = context;
        _userClaimsService = userClaimsService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<ProfileShareDto>>> GetProfileShare()
    {
        var userId = _userClaimsService.ClaimId(User);

        var profiles = await _context.HealthProfiles
            .Where(x => x.UserId == userId && x.SharedStatus > 0)
            .Select(x => new ProfileShareDto
            {
                Id = x.Id,
                Name = x.FullName
            })
            .ToListAsync();
        //if (profiles == null || !profiles.Any())
        //{
        //    return NotFound("No profiles found for the user.");
        //}

        return Ok(profiles);
    }
}
