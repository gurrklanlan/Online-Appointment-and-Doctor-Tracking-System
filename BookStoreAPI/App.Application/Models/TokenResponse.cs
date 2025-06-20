namespace App.Application.Models
{
    public class TokenResponse
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshtTokenExtTime { get; set; }
    }
}
