namespace _1CService.Application.Models.Auth.Request
{
    public struct SignUpQuery
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}