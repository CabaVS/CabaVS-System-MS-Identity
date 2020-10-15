using System;

namespace CabaVS.IdentityMS.Core.Models
{
    public class RefreshToken
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}