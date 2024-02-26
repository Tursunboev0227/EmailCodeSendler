using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmailSendlerApp.Domain.Enteties
{
    public class UserModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }
    }
}
