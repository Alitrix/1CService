namespace _1CService.Application.Models.Auth.Request
{
    public struct SignInQuery
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}