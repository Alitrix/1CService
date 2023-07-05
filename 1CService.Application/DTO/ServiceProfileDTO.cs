namespace _1CService.Application.DTO
{
    public struct ServiceProfileDto
    {
        public string ServiceAddress;
        public string ServiceBaseName;
        public string ServiceSection;

        public override string ToString()
        {
            return $"ServiceAddress: {ServiceAddress}\r\n ServiceBaseName: {ServiceBaseName}\r\n ServiceSection: {ServiceSection}";
        }
    }
}