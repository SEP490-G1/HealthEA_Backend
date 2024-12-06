using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models.DAO;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Customer
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class DocumentChatController : ControllerBase
	{
		private readonly SqlDBContext context;
		private readonly IOpenAIChatService service;
		private readonly IUserClaimsService userClaimsService;
		private readonly IMapper mapper;

		public DocumentChatController(SqlDBContext context, IOpenAIChatService service, IUserClaimsService userClaimsService, IMapper mapper)
		{
			this.context = context;
			this.service = service;
			this.userClaimsService = userClaimsService;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllMessages()
		{
			var userId = userClaimsService.ClaimId(User);
			var messages = await context.DocumentChatMessages
				.Where(x => x.UserId == userId)
				.OrderByDescending(x => x.CreatedAt)
				.ToListAsync();
			var result = mapper.Map<List<DocumentChatMessageDto>>(messages);
			return Ok(result);
		}

		[HttpPost("{docId}")]
		public async Task<IActionResult> AskAIAboutAdvice(Guid docId, [FromBody] DocumentChatMessagePostModel model)
		{
			//Get DocumentProfile
			var document = await context.DocumentProfiles
				.FirstOrDefaultAsync(x => x.Id == docId);
			if (document == null)
			{
				return BadRequest("Document does not exist!");
			}
			var userId = userClaimsService.ClaimId(User);
			if (document.UserId != userId)
			{
				return BadRequest("Document does not belong to you!");
			}
			//Create first message
			var userMessage = new DocumentChatMessage()
			{
				Message = model.Message,
				UserId = userId,
				SenderType = "USER",
			};
			context.DocumentChatMessages.Add(userMessage);
			//Send to AI first
			var instructions = new List<string>()
			{
				"You will be giving advices to a user about their blood test or urinal test.",
				"The following message is the data of the test.",
				document.ContentMedical,
				$"The user asked: '{model.Message}'",
				"Please answer in Vietnamese, do not include any Markdown in your response."
			};
			var response = await service.GetResponseAsync(instructions);
			//Create second message
			var aiMessage = new DocumentChatMessage()
			{
				Message = response,
				UserId = userId,
				SenderType = "AI",
			};
			context.DocumentChatMessages.Add(aiMessage);
			await context.SaveChangesAsync();
			return Ok(new
			{
				userMessage = mapper.Map<DocumentChatMessageDto>(userMessage),
				aiMessage = mapper.Map<DocumentChatMessageDto>(aiMessage),
			});
		}
	}
}
