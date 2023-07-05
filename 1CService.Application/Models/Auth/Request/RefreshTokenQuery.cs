namespace _1CService.Application.Models.Auth.Request
{
    public struct RefreshTokenQuery
    {
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}