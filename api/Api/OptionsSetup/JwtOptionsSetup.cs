using Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace FeedbackAnalyzer.Api.OptionsSetup;

public class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "JwtSettings";

    public void Configure(JwtOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);
    }
}