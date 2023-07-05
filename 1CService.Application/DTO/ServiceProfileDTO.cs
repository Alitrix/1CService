namespace _1CService.Application.DTO
{
    public struct ServiceProfileDTO
    {
        public string ServiceAddress;
        public string ServiceBaseName;
        public string ServiceSection;

        public override readonly string ToString()
        {
            return $"ServiceAddress: {ServiceAddress}\r\n ServiceBaseName: {ServiceBaseName}\r\n ServiceSection: {ServiceSection}";
        }
    }
}