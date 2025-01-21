public class MLServiceOptions
{
    public const string Section = "MLService";
    public string BaseUrl { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; } = 30;
}