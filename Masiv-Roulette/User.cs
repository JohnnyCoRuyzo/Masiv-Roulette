using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masiv_Roulette
{
    public class User
    {
        public Guid ID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public decimal Balance { get; set; }

        public User(string userName)
        {
            ID = Guid.NewGuid();
            UserName = userName;
            Password = GeneratePasswordAndSendEmail();
            Balance = 0;
        }

        public string GeneratePasswordAndSendEmail()
        {
            var password = GeneratePasswordString();
            SendEmail(password);
            return password;
        }

        public string GeneratePasswordString()
        {
            return "password";
        }

        public void SendEmail(string password)
        {
            string _ = "Your credentials are: " + password;
        }
    }
}
