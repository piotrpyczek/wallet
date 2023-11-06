using MongoDB.Bson.Serialization.Attributes;

namespace ExchangeRates.Domain;

public class ExchangeRate
{
    [BsonId]
    public Guid Id { get; set; }
    public string Currency { get; set; }
    public string Code { get; set; }
    public decimal Mid { get; set; }
    public DateTime Date { get; set; }
}