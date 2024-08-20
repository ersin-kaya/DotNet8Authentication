using System.Text;
using DotNet8Authentication.Constants;
using DotNet8Authentication.Data;
using DotNet8Authentication.Models;
using DotNet8Authentication.Services;
using DotNet8Authentication.Services.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
    sqlOptions.EnableRetryOnFailure()));

TokenSettings? tokenSettings = builder.Configuration.GetSection("Jwt").Get<TokenSettings>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenSettings.Issuer,
            ValidAudience = tokenSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
//     .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddSingleton<ISettingsService, SettingsService>();

var app = builder.Build();

await EnsureRolesCreatedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

// app.MapIdentityApi<ApplicationUser>();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

async Task EnsureRolesCreatedAsync()
{
    var roles = new[]
    {
        RoleConstants.Admin,
        RoleConstants.User
    };

    #region Task.WhenAll

    // The 'foreach' loop processes each role sequentially, one after the other.
    // Task.WhenAll allows processing all roles concurrently, improving performance by handling them simultaneously.
    // await Task.WhenAll(
    //     roles.Select(async role =>
    //         {
    //             using (var scope = app.Services.CreateScope())
    //             {
    //                 var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    //
    //                 if (!await roleManager.RoleExistsAsync(role))
    //                 {
    //                     await roleManager.CreateAsync(new IdentityRole(role));
    //                 }
    //                 
    //                 // A concurrency error occurred when parallel tasks were executed using Task.WhenAll,
    //                 // causing the same DbContext instance to be used by multiple threads simultaneously.
    //                 // To resolve this, a new ServiceScope should be created for each parallel task,
    //                 // allowing the RoleManager and consequently the DbContext instance to be obtained from this scope.
    //                 // This approach ensures that each task uses an independent DbContext instance, preventing the error.
    //             }
    //         })
    // );

    #endregion

    #region Parallel.ForEachAsync

    // Parallel.ForEachAsync is an asynchronous structure that executes tasks in parallel at the same time,
    // while foreach is synchronous and processes tasks sequentially.
    await Parallel.ForEachAsync(roles, async (role, cancellationToken) =>
    {
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    });

    #endregion
    
    // Parallel.ForEachAsync processes the elements of a collection in parallel and asynchronously,
    // while Task.WhenAll runs independent asynchronous tasks together and waits for all of them to complete.
    // Parallel.ForEachAsync is used for processing collections, while Task.WhenAll is used to start tasks in parallel.
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}