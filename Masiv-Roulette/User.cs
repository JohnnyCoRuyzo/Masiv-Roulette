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

        public double Balance { get; set; }

        public List<Bet> AllBets { get; set; }

        public User(string userName)
        {
            ID = Guid.NewGuid();
            UserName = userName;
            Password = GeneratePasswordAndSendEmail();
            Balance = 0;
            AllBets = new List<Bet>();
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

        public bool ValidInsertBetIntoUserHistoryBets(Bet bet)
        {
            try
            {
                AllBets.Add(bet);
                return true;
            }
            catch(Exception e)
            {
                _ = e.ToString();
                return false;
            }
        }

        public bool UserBettingAmountIsValid(double bettingAmount)
        {
            if (Balance >= bettingAmount)
            {
                Balance -= bettingAmount;
                return true;
            }
            else
                return false;
        }

        public void ChangeWiningBetStateAndReceiveMoney(Roulette currentRoulette)
        {
            CheckUserWiningBets(currentRoulette);
            List<Bet> winningBets = GetOpenWinningBets(currentRoulette);
            if (CheckIfUserHasWiningBets(winningBets)) 
                ReceiveMoney(winningBets);
        }

        public void ReceiveMoney(List<Bet> winningBets)
        {
            double winningAmount = winningBets.Sum(bet => bet.MoneyToGive());

            Balance += winningAmount;
        }

        public void CheckUserWiningBets(Roulette currentRoulette)
        {
            AllBets.ForEach(bet => bet.CheckIfBetWon(currentRoulette));
        }

        public List<Bet> GetOpenWinningBets(Roulette currentRoulette)
        {
            return AllBets.Where(bet => !bet.BetClosed && bet.ID_Roulette == currentRoulette.ID && bet.IsAWinningBet).ToList();
        }

        public bool CheckIfUserHasWiningBets(List<Bet> winningBets)
        {
            return winningBets != null;
        }

        public void ClosedRouletteBets(Roulette currentRoulette)
        {   
            AllBets.Where(bet => bet.ID_Roulette == currentRoulette.ID).ToList().ForEach(bet => bet.BetClosed = true);
        }
    }
}
