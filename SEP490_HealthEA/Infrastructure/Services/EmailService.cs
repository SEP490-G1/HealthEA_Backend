using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : INotificationService
    {
        public Task SendNotification(string to, string subject, string body)
        {
            Console.WriteLine("Email Information:");
            Console.WriteLine($"To: {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");

            return Task.CompletedTask;
        }
    }
}
