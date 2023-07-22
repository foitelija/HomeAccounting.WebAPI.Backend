using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Models.Identity
{
    public class RegistrationRequest
    {
        [Required, StringLength(16, MinimumLength = 4)]
        public string Login { get; set; }
        [Required, StringLength(50, MinimumLength = 4)]
        public string Password { get; set; }
        [Required, StringLength(16, MinimumLength = 6)]
        public string Username { get; set; }
    }
}
