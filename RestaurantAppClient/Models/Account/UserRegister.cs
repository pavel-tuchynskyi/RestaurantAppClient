using RestaurantAppClient.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAppClient.Models.Account
{
    public class UserRegister
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [Password]
        public string Password { get; set; }
    }
}
