using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAuthDemo.Data.Entities;

namespace WebAuthDemo.Data.Repositories
{
    public class UserRepository
    {
        public User Get(string username, string password)
        {
            return GetUsers().SingleOrDefault(x => x.Username == username && x.Password == password);
        }

        public User Get(string username)
        {
            return GetUsers().SingleOrDefault(x => x.Username == username);
        }

        private List<User> GetUsers()
        {
            return new List<User>
            {
                new User { Id = 1, Username = "hernan", Password = "hernan" },
                new User { Id = 2, Username = "david", Password = "david" },
                new User { Id = 3, Username = "test", Password = "test" }
            };
        }
    }
}