using System.ComponentModel.DataAnnotations;

namespace CabaVS.IdentityMS.API.DTO
{
    public class TokenRefreshRequestDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}