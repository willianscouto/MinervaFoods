namespace MinervaFoods.Domain.Entities
{
    public class Cotacao
    {
        public string Code { get; set; } = string.Empty;
        public string Codein { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal PctChange { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public decimal VarBid { get; set; }
        public long Timestamp { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
