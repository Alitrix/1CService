namespace _1CService.Application.DTO
{
    public struct ListBlankOrderDTO
    {
        public required string Number { get; init; }
        public required string Date { get; init; }
        public required string Manager { get; init; }
        public required string Contragent { get; init; }
        public int Urgency { get; set; }
        public required string CompletionDate { get; init; }
        public required string ExecuteState { get; init; }
    }
}
