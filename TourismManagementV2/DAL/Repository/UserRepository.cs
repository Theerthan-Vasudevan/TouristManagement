using Microsoft.EntityFrameworkCore;
using TourismManagementV2.DAL.Interface;
using TourismManagementV2.Models;

namespace TourismManagementV2.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TourismDBContext context;
        public UserRepository() { }

        public UserRepository(TourismDBContext context)
        {
            this.context = context;
        }
        public void addUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void deleteUser(int userId)
        {
            var user = context.Users.Find(userId);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("User Not Found");
            }
        }

        public User findById(int id)
        {
            var user = context.Users.Find(id);
            if (user != null)
            {
                return user;
            }   
            else
            {
                throw new Exception("User Not Found");
            }
        }

        public List<User> getAllUsers()
        {
            return context.Users.ToList();
        }

        public List<User> getUserBookings()
        {
            return context.Users
                           .Include(u => u.Bookings)
                           .ThenInclude(b => b.Package)
                           .ToList();
        }

        public void updateUser(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();

        }
    }
}
