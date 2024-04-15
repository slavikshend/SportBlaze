using System.ComponentModel.DataAnnotations;

namespace webapi.BLL.Models
{
    public class RegisterModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
