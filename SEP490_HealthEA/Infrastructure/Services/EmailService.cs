﻿using Domain.Interfaces.IServices;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly SqlDBContext _context;
		private readonly INoticeService service;

		public EmailService(IOptions<EmailSettings> emailSettings, SqlDBContext context, INoticeService service)
		{
			_emailSettings = emailSettings.Value;
			_context = context;
			this.service = service;
		}
		public async Task SendAppointmentEmailsAsync(string userEmail, string doctorEmail, string userName, string doctorName)
        {
            try
            {
                using (var smtpClient = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
                    smtpClient.EnableSsl = true;

                    // Gửi email cho người dùng
                    if (!string.IsNullOrEmpty(userEmail))
                    {
                        var userSubject = "LỊCH HẸN KHÁM";
                        var userBody = $"Xin chào {userName},\n\nLịch hẹn của bạn đã gửi đến bác sĩ {doctorName}.";
                        await SendEmailAsync(userEmail, userSubject, userBody);
                    }


                    // Gửi email cho bác sĩ
                    if (!string.IsNullOrEmpty(doctorEmail))
                    {
                        var doctorSubject = "LỊCH HẸN KHÁM";
                        var doctorBody = $"Xin chào bác sĩ {doctorName},\n\nĐang có bệnh nhân {userName} muốn hẹn lịch khám với bạn.";
                        await SendEmailAsync(doctorEmail, doctorSubject, doctorBody);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while sending emails: " + ex.Message);
            }
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
                    smtpClient.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_emailSettings.SenderEmail),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(recipientEmail);

                    await smtpClient.SendMailAsync(mailMessage);
                    Console.WriteLine($"Email sent to {recipientEmail}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email to {recipientEmail}: {ex.Message}");
            }
        }

        public async Task SendEmailToAllUsers(Email email, Guid reminderId)
        {
            try
            {
                var recipientEmails = _context.Reminders
                .Where(r => r.ReminderId == reminderId) 
                .Include(r => r.Events)
                    .ThenInclude(e => e.UserEvents) 
                    .ThenInclude(ue => ue.Users) 
                .SelectMany(r => r.Events.UserEvents.Select(ue => ue.Users.Email)) 
                .Distinct()
                .ToList();

                var userLists = _context.Reminders
                    .Where(r => r.ReminderId == reminderId)
                    .Include(r => r.Events)
                    .ThenInclude(e => e.UserEvents)
                    .ThenInclude(ue => ue.Users)
                .SelectMany(r => r.Events.UserEvents.Select(ue => ue.Users))
				.Distinct()
				.ToList();

                foreach (var user in userLists)
                {
					Notice userNotice = new Notice()
					{
						CreatedAt = DateTime.Now,
						Message = $"{email.Body}",
						HasViewed = false,
						NoticeId = new Guid(),
						RecipientId = user.UserId,
						UserId = user.UserId,
					};
					await service.CreateAndSendNoticeAsync(userNotice, "Nhắc nhở!");
                }


				if (!recipientEmails.Any())
                {
                    Console.WriteLine("No users found for the specified reminder time.");
                    return;
                }
                var reminder = _context.Reminders.FirstOrDefault(r => r.ReminderId == reminderId);
                if (reminder.IsSent)
                {
                    Console.WriteLine("This reminder has already been sent.");
                    return;
                }
                using (var smtpClient = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
                    smtpClient.EnableSsl = true;

                    foreach (var recipient in recipientEmails)
                    {
                        var mailMessage = new MailMessage
                        {
                            From = new MailAddress(_emailSettings.SenderEmail),
                            Subject = email.Subject,
                            Body = email.Body,
                            IsBodyHtml = true
                        };

                        mailMessage.To.Add(recipient);

                        await smtpClient.SendMailAsync(mailMessage);
                        Console.WriteLine($"Email sent to {recipient}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
