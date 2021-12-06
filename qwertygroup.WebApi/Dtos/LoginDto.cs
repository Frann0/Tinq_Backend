namespace qwertygroup.WebApi.Dtos
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public TokenDto Token { get; set; }
    }
}