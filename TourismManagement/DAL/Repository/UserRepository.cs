using TourismManagement.DAL.Interface;
using TourismManagement.Models;

namespace TourismManagement.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TourismDBContext context;

        public UserRepository(TourismDBContext dbcontext)
        {
            this.context = dbcontext;
        }

        public IEnumerable<User> GetAll() => context.Users.ToList();

        public User GetById(int id)
        {
            var userById = context.Users.Find(id);
            if(userById != null)
            {
                return userById;
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public User GetByUsername(string username)
        {
            var userName = context.Users.FirstOrDefault(u => u.Username == username);

            if (userName != null)
            {
                return userName;
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public void Add(User user)
        {
            context.Users.Add(user);
        }

        public void Update(User user)
        {
            context.Users.Update(user);
        }

        public void Delete(int id)
        {
            var user = context.Users.Find(id);
            if (user != null)
                context.Users.Remove(user);
        }

        public void Save() => context.SaveChanges();
    }
}
