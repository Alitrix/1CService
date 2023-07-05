namespace _1CService.Application.Models.BlankOrderModel.Request
{
    public struct AddCommentToBlankOrderCommand
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public string User1C { get; set; }
        public string Comment { get; set; }
    }
}