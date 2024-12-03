using Domain.Interfaces.IServices;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenCallsController : ControllerBase
    {
        private readonly SqlDBContext _context;
        private readonly IUserClaimsService userClaimsService;

        public TokenCallsController(SqlDBContext context, IUserClaimsService userClaimsService)
        {
            _context = context;
            this.userClaimsService = userClaimsService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetInforCaller()
        {
            var userId = userClaimsService.ClaimId(User);
            var inforCaller = await _context.Users.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            return Ok(inforCaller);
        }
        [HttpPost("call-info")]
        public async Task<IActionResult> GetCallerAndCalleeInfo([FromBody] CallInfoRequest request)
        {
            var caller = await _context.Users
                .Where(x => x.UserId == request.CallerID)
                .FirstOrDefaultAsync();

            var doctor = await _context.Doctors
                .Where(x => x.Id == request.CalleeUserId)
                .FirstOrDefaultAsync();
            var callee = await _context.Users
                .Where(x => x.UserId == doctor.UserId)
                .FirstOrDefaultAsync();

            if (caller == null || callee == null)
            {
                return NotFound("Caller or Callee not found");
            }

            var result = new
            {
                tokenCall = caller.TokenCall,
                callerId = caller.CallerId,
                calleeId = callee.CallerId,
                nameCallee = callee.FirstName + " " + callee.LastName,
            };

            return Ok(result);
        }
        [HttpPost("doctor-call")]
        public async Task<IActionResult> DoctorCall([FromBody] CallInfoRequest request)
        {
            var caller = await _context.Users
                .Where(x => x.UserId == request.CallerID)
                .FirstOrDefaultAsync();

            var doctor = await _context.Doctors
                .Where(x => x.UserId == request.CalleeUserId)
                .FirstOrDefaultAsync();
            var callee = await _context.Users
                .Where(x => x.UserId == doctor.UserId)
                .FirstOrDefaultAsync();

            if (caller == null || callee == null)
            {
                return NotFound("Caller or Callee not found");
            }

            var result = new
            {
                tokenCall = caller.TokenCall,
                callerId = caller.CallerId,
                calleeId = callee.CallerId,
                nameCallee = caller.FirstName + " " + caller.LastName,
            };

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTokenCall()
        {
            int userCount = await _context.Users.CountAsync();
            int Id = userCount + 1;

            string calledId = $"user{Id}";
            var tokenCalls = await _context.TokenCalls.Where(t => t.CallerId == calledId)
               .ToListAsync();
            foreach (var calls in tokenCalls)
            {
                var user = new User
                {
                    TokenCall = calls.TokenCall, 
                    CallerId = calls.CallerId    
                };

                _context.Users.Add(user);
            }
            _context.TokenCalls.RemoveRange(tokenCalls);
            int userCou12nt = await _context.TokenCalls.CountAsync();
            //await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
