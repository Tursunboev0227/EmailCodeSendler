using EmailSendlerApp.Domain.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSendlerApp.Application.Services
{
    public interface IEmailService
    {
        public Task SendMessageAsync(EmailModel model);
        public Task<bool> CheckEmailAsync(string password);
        public Task AddUser(UserModel user);
        public Task<bool> ChechUsersStatus(string email);
        public Task<bool> VerifyCredentials(UserModel user);
             
        
    }
}
