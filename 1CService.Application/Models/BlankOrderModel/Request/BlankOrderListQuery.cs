namespace _1CService.Application.Models.BlankOrderModel.Request
{
    public struct BlankOrderListQuery
    {
        public string WorkInPlace { get; set; }
        public BlankOrderListQuery(string place) => WorkInPlace = place;
    }
}
