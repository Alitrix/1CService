namespace _1CService.Application.DTO
{
    public struct Settings
    {
        public string User1C;
        public string Password1C;
        public string ServiceAddress;
        public string ServiceBaseName;
        public string ServiceSection;

        public override string ToString()
        {
            return $"User:{User1C}\r\n ServiceAddress: {ServiceAddress}\r\n ServiceBaseName: {ServiceBaseName}\r\n ServiceSection: {ServiceSection}";
        }
    }
}