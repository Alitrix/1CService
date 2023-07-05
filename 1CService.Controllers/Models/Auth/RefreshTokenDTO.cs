namespace _1CService.Controllers.Models.Auth
{
    public struct RefreshTokenDTO
    {
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
