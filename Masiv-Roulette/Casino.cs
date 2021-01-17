using System;
using System.Collections.Generic;
using System.Linq;
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
            var rng = new Random();
            AllRoulletes = Enumerable.Range(1, lotSize).Select(index => new Roulette()).ToList();
            return AllRoulletes;
        }

        public Roulette GetRouletteById(Guid Id)
        {
            return AllRoulletes.Where(roulette => roulette.ID == Id).FirstOrDefault();
        }

        public bool RoulleteExists(Guid Id)
        {
            return AllRoulletes.Where(roulette => roulette.ID == Id).Count() > 0;
        }

        public Guid CreateUserInCasino(string userName)
        {
            User userToCreate = new User(userName);
            AllUsers.Add(userToCreate);
            return userToCreate.ID;
        }

        public User GetUserById(Guid Id)
        {
            return AllUsers.Where(user => user.ID == Id).FirstOrDefault();
        }

        public bool UserExists(Guid Id)
        {
            return AllUsers.Where(user => user.ID == Id).Count() > 0;
        }

        public bool AuthenticateUserInCasino(string requestCredentials, Guid userId)
        {
            if (UserExists(userId))
            {
                User userBeginAuthenticated = GetUserById(userId);
                return AuthorizeCredentials(userBeginAuthenticated, requestCredentials);
            }
            else
                return false;
        }

        public bool AuthorizeCredentials(User userBeginAuthenticated, string requestCredentials)
        {
            byte[] credentialBytes = Convert.FromBase64String(requestCredentials);
            string[] credentialsOfAuthentication = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            string userNameOfAuthentication = credentialsOfAuthentication[0];
            string passwordOfAuthentication = credentialsOfAuthentication[1];
            return userBeginAuthenticated.ValidatePasswordForUserBeginAuthenticated(passwordOfAuthentication)
                && userBeginAuthenticated.ValidateUserNameForUserBeginAuthenticated(userNameOfAuthentication) ;
        }
    }
}
