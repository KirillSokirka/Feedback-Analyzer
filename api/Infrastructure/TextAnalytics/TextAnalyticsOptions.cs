using Azure;

namespace Infrastructure.TextAnalytics;

public class TextAnalyticsOptions
{
    public AzureKeyCredential SecretKey { get; set; } = null!;
    public Uri Endpoint { get; set; } = null!;
}