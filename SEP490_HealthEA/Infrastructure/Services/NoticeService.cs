using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class NoticeService : INoticeService
	{
		private readonly SqlDBContext context;
		private readonly FirebaseNotificationService firebaseService;

		public NoticeService(SqlDBContext context, FirebaseNotificationService firebaseService)
		{
			this.context = context;
			this.firebaseService = firebaseService;
		}

		public async Task CreateAndSendNoticeAsync(Notice notice, string title)
		{
			context.Notices.Add(notice);
			await context.SaveChangesAsync();
			//Send to Firebase
			var deviceTokens = await context.DeviceTokens
				.Where(dt => dt.UserId == notice.RecipientId)
				.Select(dt => dt.DeviceToken)
				.ToListAsync();
			Console.WriteLine($"User: {notice.RecipientId} has {deviceTokens.Count} token(s).");
			foreach (var deviceToken in deviceTokens)
			{
				await firebaseService.SendNotificationAsync(deviceToken, title, notice.Message);
			}
		}
	}
}
