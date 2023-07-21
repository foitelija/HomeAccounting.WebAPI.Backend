using System.ComponentModel.DataAnnotations;

namespace HomeAccounting.Domain.Users
{
    public class UserRegister
    {
        [Required, StringLength(16, MinimumLength = 4)]
        public string Login { get; set; }
        [Required, StringLength(50, MinimumLength = 4)]
        public string Password { get; set; }
        [Required, StringLength(16, MinimumLength = 6)]
        public string Username { get; set; }
    }
}
