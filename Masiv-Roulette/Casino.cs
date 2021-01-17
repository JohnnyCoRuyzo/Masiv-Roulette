using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Masiv_Roulette
{
    public class Casino
    {
        public Guid ID { get; set; }

        public List<Roulette> AllRoulletes { get; set; }

        public List<User> AllUsers { get; set; }

        public const int LOT_DEFAULT_SIZE = 5;

        public Casino()
        {
            ID = Guid.NewGuid();
            AllRoulletes = CreateCasinoRoulettesByLot(LOT_DEFAULT_SIZE);
        }

        public List<Roulette> CreateCasinoRoulettesByLot(int lotSize)
        {
            AllRoulletes = Enumerable.Range(1, lotSize).Select(index => new Roulette()).ToList();
            return AllRoulletes;
        }

        public Guid CreateCasinoSingleRoulette()
        {
            Roulette roullete = new Roulette();
            AllRoulletes.Add(roullete);
            return roullete.ID;
        }

        public bool IsRouletteOpenById(Guid Id)
        {
            if (RoulleteExistsById(Id))
            {
                Roulette roullete = GetRouletteById(Id);
                if (roullete.IsRouletteOpen)
                    return true;
            }
            return false;
        }

        private Roulette GetRouletteById(Guid Id)
        {
            return AllRoulletes.Where(roulette => roulette.ID == Id).FirstOrDefault();
        }

        private Roulette GetRandomRouletteByOpenStatus(bool openStatus)
        {
            return AllRoulletes.Where(roulette => roulette.IsRouletteOpen == openStatus).FirstOrDefault();
        }

        private bool RoulleteExistsById(Guid Id)
        {
            return AllRoulletes.Where(roulette => roulette.ID == Id).Count() > 0;
        }

        private bool RoulleteExists(Roulette roullete)
        {
            return roullete != null;
        }

        public Guid CreateUserInCasino(string userName)
        {
            User userToCreate = new User(userName);
            AllUsers.Add(userToCreate);
            return userToCreate.ID;
        }

        private User GetUserById(Guid Id)
        {
            return AllUsers.Where(user => user.ID == Id).FirstOrDefault();
        }

        public User GetUserByUserName(string userName)
        {
            return AllUsers.Where(user => user.UserName == userName).FirstOrDefault();
        }

        private bool UserExistsById(Guid Id)
        {
            return AllUsers.Where(user => user.ID == Id).Count() > 0;
        }

        public bool UserExistsByUserName(string userName)
        {
            return AllUsers.Where(user => user.UserName == userName).Count() > 0;
        }

        public User GetUserAuthenticated(HttpRequest request)
        {
            Guid userId = Guid.Parse(request.Headers["userID"].ToString());
            return GetUserById(userId);
        }

        public bool AuthenticateRequest(HttpRequest request)
        {
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(request.Headers["Authorization"]);
                Guid userId = Guid.Parse(request.Headers["userID"].ToString());
                return AuthenticateUserByIdInCasino(authHeader.Parameter, userId);
            }
            catch(Exception e)
            {
                _ = e.ToString();
                return false;
            }
        }

        private bool AuthenticateUserByIdInCasino(string requestCredentials, Guid userId)
        {
            try
            {
                string[] credentialsOfAuthentication = GetUserAndPasswordOfCredentials(requestCredentials);
                if (UserExistsById(userId))
                {
                    User userBeginAuthenticated = GetUserById(userId);
                    return AuthorizeCredentials(userBeginAuthenticated, credentialsOfAuthentication);
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                _ = e.ToString();
                return false;
            }
        }

        private bool AuthorizeCredentials(User userBeginAuthenticated, string[] credentialsOfAuthentication)
        {
            string userNameOfAuthentication = credentialsOfAuthentication[0];
            string passwordOfAuthentication = credentialsOfAuthentication[1];
            return userBeginAuthenticated.ValidatePasswordForUserBeginAuthenticated(passwordOfAuthentication)
                && userBeginAuthenticated.ValidateUserNameForUserBeginAuthenticated(userNameOfAuthentication);
        }

        private string[] GetUserAndPasswordOfCredentials(string requestCredentials)
        {
            try
            {
                byte[] credentialBytes = Convert.FromBase64String(requestCredentials);
                return Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Bet CreateCurrentCasinoBet(Bet currentBet)
        {
            currentBet.ID = Guid.NewGuid();
            return currentBet;
        }

        public bool ValidInsertBetIntoOpenRoulette(Bet currentBet, User userAuthenticated)
        {
            Roulette currentRoullete = GetRandomRouletteByOpenStatus(true);
            if (RouletteExistsAndIsOpen(currentRoullete))
                return ValidInsertIntoRouletteAndUserBets(currentBet, userAuthenticated, currentRoullete);
            return false;
        }

        public bool ValidInsertIntoRouletteAndUserBets(Bet currentBet, User userAuthenticated, Roulette currentRoullete)
        {
            currentBet.ID_User = userAuthenticated.ID;
            currentBet.ID_Roulette = currentRoullete.ID;
            return userAuthenticated.ValidInsertBetIntoUserHistoryBets(currentBet)
                && currentRoullete.ValidInsertBetIntoRouletteHistoryBets(currentBet);
        }

        private bool RouletteExistsAndIsOpen(Roulette currentRoullete)
        {
            return RoulleteExists(currentRoullete)
                && IsRouletteOpenById(currentRoullete.ID);
        }

        public bool PlaceValidBetInCasinoRoulleteWithAuthentication(Bet bettingContent, HttpRequest request)
        {
            if (AuthenticateRequest(request))
            {
                return ValidInsertUserBetIntoOpenRoulette(bettingContent, request);
            }
            return false;
        }

        private bool ValidInsertUserBetIntoOpenRoulette(Bet bettingContent, HttpRequest request)
        {
            Bet currentBet = CreateCurrentCasinoBet(bettingContent);
            User userAuthenticated = GetUserAuthenticated(request);
            if (BetIsValid(userAuthenticated, currentBet))
            {
                return ValidInsertBetIntoOpenRoulette(currentBet, userAuthenticated);
            }
            return false;
        }

        public bool BetIsValid(User userAuthenticated, Bet currentBet)
        {
            return userAuthenticated.UserBettingAmountIsValid(currentBet.BettingAmount)
                   && currentBet.IsValidBet();
        }

        public List<Bet> GetBetsOfClosedRoulette(Guid id)
        {
            Roulette roulleteToClose = GetRouletteById(id);
            roulleteToClose.SpinRoulette();
            List<Bet> betsAfterRouletteSpin = roulleteToClose.CheckWiningBetsInRoullete();
            roulleteToClose.CloseRoulette();
            return betsAfterRouletteSpin;
        }
    }
}
