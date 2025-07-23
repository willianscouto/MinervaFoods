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


        public Cotacao GerarCotacaoFake(string moeda)
        {
            moeda = moeda.ToUpperInvariant();

            return moeda switch
            {
                "USD" => new Cotacao
                {
                    Code = "USD",
                    Codein = "BRL",
                    Name = "Dólar Americano/Real Brasileiro",
                    High = 5.45m,
                    Low = 5.30m,
                    PctChange = 0.25m,
                    Bid = 5.38m,
                    Ask = 5.40m,
                    VarBid = 0.02m,
                    Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    CreateDate = DateTime.Now
                },
                "EUR" => new Cotacao
                {
                    Code = "EUR",
                    Codein = "BRL",
                    Name = "Euro/Real Brasileiro",
                    High = 6.10m,
                    Low = 6.00m,
                    PctChange = 0.12m,
                    Bid = 6.05m,
                    Ask = 6.07m,
                    VarBid = 0.01m,
                    Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    CreateDate = DateTime.Now
                },
                "BRL" => new Cotacao
                {
                    Code = "BRL",
                    Codein = "USD",
                    Name = "Real Brasileiro/Dólar Americano",
                    High = 0.189m,
                    Low = 0.182m,
                    PctChange = -0.15m,
                    Bid = 0.185m,
                    Ask = 0.187m,
                    VarBid = -0.003m,
                    Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    CreateDate = DateTime.Now
                },
                _ => new Cotacao
                {
                    Code = "UNKNOWN",
                    Codein = "UNKNOWN",
                    Name = "Moeda desconhecida",
                    High = 0,
                    Low = 0,
                    PctChange = 0,
                    Bid = 0,
                    Ask = 0,
                    VarBid = 0,
                    Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    CreateDate = DateTime.Now
                }
            };
        }
    }
}
