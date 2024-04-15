using System.ComponentModel.DataAnnotations;

namespace webapi.BLL.Models
{
    public class UserModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
    }
}
