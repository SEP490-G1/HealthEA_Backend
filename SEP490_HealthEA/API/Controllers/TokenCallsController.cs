﻿using Domain.Interfaces.IServices;
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
            var test = inforCaller.TokenCall;
            return Ok(inforCaller);
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