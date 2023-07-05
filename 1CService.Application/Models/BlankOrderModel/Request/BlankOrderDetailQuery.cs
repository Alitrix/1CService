namespace _1CService.Application.Models.BlankOrderModel.Request
{
    public class BlankOrderDetailQuery
    {
        public string Number { get; set; }

        public string Date { get; set; }

        public BlankOrderDetailQuery(string number, string date)
        {
            Number = number;
            Date = date;
        }
    }
}
