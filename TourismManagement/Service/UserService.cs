using TourismManagement.DAL.Interface;
using TourismManagement.Models;

namespace TourismManagement.Service
{
    public class UserService
    {
        private readonly IUserRepository userRepo;

        public UserService(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return userRepo.GetAll();
        }

        public User GetUserById(int id)
        {
            return userRepo.GetById(id);
        }

        public void RegisterUser(User user)
        {
            // Business rule: default role = Customer
            user.Role = "Customer";
            user.PasswordHash = HashPassword(user.PasswordHash); // hashing
            userRepo.Add(user);
            userRepo.Save();
        }

        public User Authenticate(string username, string password)
        {
            var user = userRepo.GetByUsername(username);
            if (!VerifyPassword(password, user.PasswordHash))
                throw new Exception("Invalid credentials.");
            return user;
        }

        private string HashPassword(string password)
        {
            // ⚠️ For demo only: use a secure hashing algorithm in real apps
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
