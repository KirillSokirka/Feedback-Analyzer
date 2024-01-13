using Azure;
using Infrastructure.TextAnalytics;
using Microsoft.Extensions.Options;

namespace FeedbackAnalyzer.Api.OptionsSetup;

public class TextAnalyticsOptionsSetup : IConfigureOptions<TextAnalyticsOptions>
{
    private const string SectionName = "AzureTextAnalyticsSettings";

    private readonly IConfiguration _configuration;

    public TextAnalyticsOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(TextAnalyticsOptions options)
    {
        var secretKey = _configuration[$"{SectionName}:SecretKey"] ?? throw new Exception("Missing SecretKey setting");
        var endpoint = _configuration[$"{SectionName}:Endpoint"] ?? throw new Exception("Missing Endpoint setting");

        options.SecretKey = new AzureKeyCredential(secretKey);
        options.Endpoint = new Uri(endpoint);
    }
}