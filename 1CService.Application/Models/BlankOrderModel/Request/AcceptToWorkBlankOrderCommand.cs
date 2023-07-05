namespace _1CService.Application.Models.BlankOrderModel.Request
{
    public struct AcceptToWorkBlankOrderCommand
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public int Status { get; set; }
    }
}