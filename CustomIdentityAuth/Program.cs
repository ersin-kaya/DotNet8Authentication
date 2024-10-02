using System.Reflection;
using System.Text;
using CustomIdentityAuth.Constants;
using CustomIdentityAuth.Data;
using CustomIdentityAuth.Entities.Concretes;
using CustomIdentityAuth.Filters;
using CustomIdentityAuth.Services;
using CustomIdentityAuth.Services.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
//     .AddEntityFrameworkStores<ApplicationDbContext>();

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key)),
            ClockSkew = TimeSpan.Zero // This sets the time tolerance for JWT validation to zero, meaning the token must be valid exactly within the specified time frame, with no deviations allowed.
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<ISettingsService, SettingsService>();
builder.Services.AddScoped<UpdateLastActivityFilter>();
// builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<UpdateLastActivityFilter>();
});
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\n\nExample: \"Bearer abcdef12345\""
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

await EnsureRolesCreatedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.MapIdentityApi<ApplicationUser>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

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
    .WithOpenApi()
    .RequireAuthorization();

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