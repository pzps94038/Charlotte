namespace Charlotte.Model
{
    public class Token
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public Token(string accessToken, string refreshToken) 
        {
            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;
        }
    }
}
