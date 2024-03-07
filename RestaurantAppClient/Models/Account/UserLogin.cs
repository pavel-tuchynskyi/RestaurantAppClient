using RestaurantAppClient.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAppClient.Models.Account
{
    public class UserLogin
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [Password]
        public string Password { get; set; }
    }
}
