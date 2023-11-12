namespace Wallet.Implementation.DataObjects;

public class WalletDTO
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<CurrencyBucketDTO> Currencies { get; set; }
}