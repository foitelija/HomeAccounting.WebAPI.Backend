using System.ComponentModel.DataAnnotations;

namespace HomeAccounting.Domain.Users
{
    public class UserLogin
    {
        [Required]
        public string Login { get;set; }
        [Required]
        public string Password { get;set; }
    }
}
