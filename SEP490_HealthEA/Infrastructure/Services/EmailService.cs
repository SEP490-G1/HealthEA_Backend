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
        public void SendAppointmentEmails(string userEmail, string doctorEmail, string userName, string doctorName)
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
                        SendEmail(userEmail, userSubject, userBody);
                    }


                    // Gửi email cho bác sĩ
                    if (!string.IsNullOrEmpty(doctorEmail))
                    {
                        var doctorSubject = "LỊCH HẸN KHÁM";
                        var doctorBody = $"Xin chào bác sĩ {doctorName},\n\nĐang có bệnh nhân {userName} muốn hẹn lịch khám với bạn.";
                        SendEmail(doctorEmail, doctorSubject, doctorBody);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while sending emails: " + ex.Message);
            }
        }
      
        public void SendEmail(string recipientEmail, string subject, string body)
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

                    smtpClient.Send(mailMessage);
                    Console.WriteLine($"Email sent to {recipientEmail}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email to {recipientEmail}: {ex.Message}");
            }
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
