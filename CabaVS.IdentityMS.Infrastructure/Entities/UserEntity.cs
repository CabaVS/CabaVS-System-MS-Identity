using System;

namespace CabaVS.IdentityMS.Infrastructure.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsBlocked { get; set; }
    }
}