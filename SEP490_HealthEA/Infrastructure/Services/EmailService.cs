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

        public EmailService(IOptions<EmailSettings> emailSettings, SqlDBContext context)
        {
            _emailSettings = emailSettings.Value;
            _context = context;
        }

        public void SendEmailToAllUsers(Email email, DateTime reminderTime)
        {
            try
            {
                var recipientEmails = _context.Reminders
                .Where(r => r.ReminderTime == reminderTime) 
                .Include(r => r.Events)
                    .ThenInclude(e => e.UserEvents) 
                    .ThenInclude(ue => ue.Users) 
                .SelectMany(r => r.Events.UserEvents.Select(ue => ue.Users.Email)) 
                .Distinct()
                .ToList();

                if (!recipientEmails.Any())
                {
                    Console.WriteLine("No users found for the specified reminder time.");
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

                        smtpClient.Send(mailMessage);
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
