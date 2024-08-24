using TeleHome.Models;

namespace TeleHome
{
    public class UserManagementService
    {
        private readonly Dictionary<string, User> _users;

        public UserManagementService()
        {
            _users = new Dictionary<string, User>();
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            User user;
            _users.TryGetValue(email, out user);

            return await Task.FromResult(user);
        }
    }
    //public class User
    //{
    //    public string Email { get; set; }
    //    public string PasswordHash { get; set; }
    //}
}
