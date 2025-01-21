using Microsoft.Extensions.Options;

public class SentimentService:ISentimentService
{
    private readonly IOptions<MLServiceOptions> _options;
    private readonly ILogger<SentimentService> _logger;
    private readonly IHttpClientFactory _clientFactory;

    public SentimentService(IHttpClientFactory clientFactory,IOptions<MLServiceOptions> options, ILogger<SentimentService> logger)
    {
        _clientFactory = clientFactory;
        _options = options;
        _logger = logger;
    }


    public async Task<SentimentResponse> AnalyzeSentimentAsync(SentimentRequest request)
    {
        var client = _clientFactory.CreateClient("MLService");

        try
        {
            _logger.LogInformation("Analyzing sentiment for text: {TextLength} chars", request.Text.Length);
            

            var response = await client.PostAsJsonAsync("/analyze", request);
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadFromJsonAsync<SentimentResponse>()
                ?? throw new Exception("Failed to deserialize response");
        }
        catch (Exception)
        {
            // Temporary fallback until Python service is ready
            return new SentimentResponse(request.Text, "Positive", 0.8);
        }

    }
}