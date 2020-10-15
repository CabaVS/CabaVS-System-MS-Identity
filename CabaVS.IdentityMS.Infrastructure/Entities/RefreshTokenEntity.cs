using System;

namespace CabaVS.IdentityMS.Infrastructure.Entities
{
    public class RefreshTokenEntity
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}