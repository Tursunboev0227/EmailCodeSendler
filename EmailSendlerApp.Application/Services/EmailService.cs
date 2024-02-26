using EmailSendlerApp.Domain.Enteties;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailSendlerApp.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;   
        }

        public async Task AddUser(UserModel user)
        {
            string file = @"C:\Users\USER\Desktop\EntiteFramework\EmailSendler\EmailSendlerApp.Application\AllUsers.txt";

            string User = $"{user.Email},{user.Password}\n";

            string[] userEntries = await File.ReadAllLinesAsync(file);

            foreach (var entry in userEntries)
            {
                var parts = entry.Split(',');

                if (parts.Length == 2 && parts[0] == user.Email)
                {
                    return;
                }
            }
            await File.AppendAllTextAsync(file, User);
        }

        public  async Task<bool> ChechUsersStatus(string email)
        {
            string AllUsersPath = @"C:\Users\USER\Desktop\EntiteFramework\EmailSendler\EmailSendlerApp.Application\AllUsers.txt";
            string[] AllUsers = await File.ReadAllLinesAsync(AllUsersPath);
            foreach (string User in AllUsers)
            {
                var temp = User.Split(',');
                if(temp.Length == 2 && temp[0] == email) return true;
            }
            return false;
        }

        public async Task<bool> CheckEmailAsync(string password)
        {
            return password == "Nma gap";
        }

        public async Task SendMessageAsync(EmailModel model)
        {
            var emailSettings = _config.GetSection("EmailSettings");

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings["Sender"], emailSettings["SenderName"]),
                Subject = model.Subject,
                Body = "Password to verify your code",
                IsBodyHtml = true
            };

            using var smtpClient = new SmtpClient(emailSettings["Sender"], int.Parse(emailSettings["Password"]))
            {
                Port = Convert.ToInt32(emailSettings["MailPort"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(emailSettings["Sender"], emailSettings["Password"]),
                EnableSsl = true
            };

        }

        public async Task<bool> VerifyCredentials(UserModel user)
        {
            string file = @"C:\Users\USER\Desktop\EntiteFramework\EmailSendler\EmailSendlerApp.Application\AllUsers.txt";

            string[] AllUsers = await File.ReadAllLinesAsync(file);

            foreach (var User in AllUsers)
            {
                var parts = User.Split(',');

                if (parts.Length == 2 && parts[0] == user.Email && parts[1] == user.Password)
                {
                    return true;
                }
            }

            return false;
        }
    }
}