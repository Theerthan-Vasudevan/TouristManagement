using TourismManagementV2.Models;

namespace TourismManagementV2.DAL.Interface
{
    public interface IUserRepository
    {
        void addUser(User user);
        void deleteUser(int userId);
        void updateUser(User user);
        List<User> getAllUsers();
        User findById(int id);

        List<User> getUserBookings();
    }
}
