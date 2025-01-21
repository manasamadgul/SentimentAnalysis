using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
// Add service registration
builder.Services.AddScoped<ISentimentService, SentimentService>();
builder.Services.Configure<MLServiceOptions>(
builder.Configuration.GetSection(MLServiceOptions.Section));

// Register HTTP client
builder.Services.AddHttpClient("MLService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("MLService:BaseUrl").Value!);
    client.Timeout = TimeSpan.FromSeconds(30);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/sentiment", async (ISentimentService sentimentService, SentimentRequest request)=>
{
    try
    {
        if (!request.IsValid)
        {
            return Results.BadRequest(new { error = "Text cannot be empty or whitespace" });
        }

        var result = await sentimentService.AnalyzeSentimentAsync(request);
        return Results.Ok(result);
    }
    catch (Exception)
    {
        return Results.Problem("An internal error occurred");
    }
});

app.Run();

public record SentimentRequest
{
    public required string Text { get; init; }

    public bool IsValid => !string.IsNullOrWhiteSpace(Text);
}


public record SentimentResponse(string Text, string Sentiment, double Score);