using _1CService.Domain.Models;

namespace _1CService.Application.Models.BlankOrderModel.Responses
{
    public struct BlankOrderDetail
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public string Manager { get; set; }
        public string Contragent { get; set; }
        public int Urgency { get; set; }
        public string CompletionDate { get; set; }
        public int ExecuteState { get; set; }
        public byte[] ImagePreview { get; set; }
        public List<Material> Materials { get; set; }
        public List<Product> Products { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
