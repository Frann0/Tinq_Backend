using System;

namespace qwertygroup.WebApi.Helpers
{
    public class UsernameGenerator
    {
        public static string GenerateRandomUsername()
        {
            var rand = new Random();
            return "test" + rand.Next(99999999);
        }
    }
}