using FeedbackAnalyzer.Api.Endpoints;
using FeedbackAnalyzer.Api.OptionsSetup;
using FeedbackAnalyzer.Application;
using FeedbackAnalyzer.Application.Abstraction;
using Identity;
using Identity.DbContext;
using Identity.Models;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.DbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IdentityDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDbConnection")));

builder.Services.AddDbContext<ApplicationDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDatabaseContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    await IdentityDatabaseInitializer.Initialize(scope.ServiceProvider);
    await ApplicationDatabaseInitializer.Initialize(scope.ServiceProvider);
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.AddAuthenticationEndpoints();

app.Run();