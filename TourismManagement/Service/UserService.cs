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
            user.Role = "Customer";
            //if (string.IsNullOrWhiteSpace(user.Password))
            //    throw new Exception("Password is required.");

            //user.PasswordHash = HashPassword(user.Password);
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

        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return null;

            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
