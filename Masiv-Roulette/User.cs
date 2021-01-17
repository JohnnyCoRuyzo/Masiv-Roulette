using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Masiv_Roulette
{
    public class User
    {
        public Guid ID { get; set; }

        public string UserName { get; set; }

        public byte[] Password { get; set; }

        public decimal Balance { get; set; }

        public User(string userName)
        {
            ID = Guid.NewGuid();
            UserName = userName;
            Password = GeneratePasswordAndSendEmail();
            Balance = 0;
        }

        public byte[] GeneratePasswordAndSendEmail()
        {
            var password = GeneratePasswordString();
            SendEmail(password);
            return EncodePassword(password);
        }

        public string GeneratePasswordString()
        {
            return "password";
        }

        public void SendEmail(string password)
        {
            string _ = "Your credentials are: " + password;
        }

        public byte[] EncodePassword(string loginPassword)
        {
            byte[] passwordAsBytes = Encoding.ASCII.GetBytes(loginPassword); ;
            byte[] encodingResult;
            SHA256 sha256Managed = new SHA256Managed();
            encodingResult = sha256Managed.ComputeHash(passwordAsBytes);
            return encodingResult;
        }

        public bool ValidatePasswordForUserBeginAuthenticated(string passwordOfAuthentication)
        {
            if (ByteArrayCompare(EncodePassword(passwordOfAuthentication), Password))
                return true;
            return false;
        }

        public bool ValidateUserNameForUserBeginAuthenticated(string userNameOfAuthentication)
        {
            if (userNameOfAuthentication == UserName)
                return true;
            return false;
        }

        static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            return StructuralComparisons.StructuralEqualityComparer.Equals(a1, a2);
        }
    }
}
