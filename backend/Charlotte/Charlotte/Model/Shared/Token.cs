namespace Charlotte.Model
{
    public class Token
    {
        public string? accessToken { get; set; }
        public string? refreshToken { get; set; }
        public Token() { }
        public Token(string accessToken, string refreshToken) 
        {
            this.accessToken = accessToken;
            this.refreshToken = refreshToken;
        }
    }
}
