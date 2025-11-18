using SmartManage.Components.Models;

namespace SmartManage.Components.Service
{
    public class CurrentUserService
    {
        public int? UserId { get; private set; }
        public string? Email { get; private set; }
        public string? FirstName { get; private set; }

        public bool IsAuthenticated => UserId.HasValue;

        public void SetUser(User user)
        {
            UserId = user.UserId;
            Email = user.Email;
            FirstName = user.FirstName;
        }

        public void Clear()
        {
            UserId = null;
            Email = null;
            FirstName = null;
        }
    }
}
