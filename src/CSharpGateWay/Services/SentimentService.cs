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
            _logger.LogInformation("Sending request to ML service for text: {TextStart}...", 
                request.Text[..Math.Min(50, request.Text.Length)]);
            
            var response = await client.PostAsJsonAsync("/analyze", request);
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("ML service returned status code: {StatusCode}", response.StatusCode);
                throw new Exception($"ML service error: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<SentimentResponse>()
                ?? throw new Exception("Failed to deserialize response");

        }
        catch (Exception ex)
        {
             _logger.LogError(ex, "Error processing sentiment analysis");
            throw;
        }

    }
}