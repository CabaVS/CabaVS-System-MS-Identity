using CabaVS.IdentityMS.Core.Configuration;
using System.ComponentModel.DataAnnotations;

namespace CabaVS.IdentityMS.API.DTO
{
    public class TokenCreateRequestDto
    {
        [Required]
        [MaxLength(MaxLengthConstraints.User.Username)]
        public string Username { get; set; }

        [Required]
        [MaxLength(MaxLengthConstraints.User.Password)]
        public string Password { get; set; }

        public void Deconstruct(out string username, out string password)
        {
            username = Username;
            password = Password;
        }
    }
}