namespace _1CService.Controllers.Models.BlankOrder.Command
{
    public struct AcceptInWorkBlankOrderDTO
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public int Status { get; set; }
    }
}
