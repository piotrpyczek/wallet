namespace ExchangeRates.BackgroundTasks.Models;

public class Table
{
    public IEnumerable<Rate> Rates { get; set; } = new List<Rate>();
}