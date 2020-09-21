using System;

namespace Consumption.Console
{
    public enum AuthScheme
    {
        NONE,
        BASIC,
        BEARER
    }

    public class AuthData
    {
        public AuthScheme Scheme { get; set; }
        public string UPN { get; set; }
        public string PasswordOrToken { get; set; }
        public DateTime ValidTo { get; set; }
    }
}