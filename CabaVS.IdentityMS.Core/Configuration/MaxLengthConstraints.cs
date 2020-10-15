namespace CabaVS.IdentityMS.Core.Configuration
{
    public static class MaxLengthConstraints
    {
        public static class User
        {
            public const int Email = 128;
            public const int Username = 64;
            public const int Password = 64;
        }

        public const int RefreshToken = 64;
    }
}