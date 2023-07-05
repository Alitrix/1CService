using _1CService.Application.DTO;

namespace _1CService.Application.Models.Auth.Response
{
    public struct JwtAuthToken
    {
        public Tokens Access_Tokens { get; set; }
        public long TimeExp { get; set; }
        public string Error { get; set; }
    }
}
