namespace _1CService.Application.Models.Auth.Response
{
    public struct ResponseMessage
    {
        public string Message { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; }
    }
}