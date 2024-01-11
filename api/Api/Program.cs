using FeedbackAnalyzer.Api.OptionsSetup;
using Identity.DbContext;
using Identity.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IdentityDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDbConnection")));

builder.Services.AddDbContext<ApplicationDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbConnection")));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    await IdentityDatabaseInitializer.Initialize(scope.ServiceProvider);
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();