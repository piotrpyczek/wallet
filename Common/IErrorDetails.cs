namespace Common;

public interface IErrorDetails
{
  public int StatusCode { get; set; }
  public string Detail { get; set; }
  public IReadOnlyDictionary<string, string[]> Errors { get; set; }
}
