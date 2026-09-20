using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MediatR;
using TrackFlow.Application.Interfaces;
using TrackFlow.Infrastructure.Data;
using TrackFlow.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
var jwtSecret = builder.Configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT secret is not configured");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "TrackFlow";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "TrackFlowClient";

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string is not configured");
builder.Services.AddDbContext<TrackFlowDbContext>(options =>
    options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.MigrationsAssembly("TrackFlow.WebAPI")));

// JWT Authentication
var key = Encoding.ASCII.GetBytes(jwtSecret);
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// MediatR
var trackFlowAssemblies = AppDomain.CurrentDomain.GetAssemblies()
    .Where(a => a.GetName().Name?.StartsWith("TrackFlow") == true)
    .ToArray();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(trackFlowAssemblies));


// Services
builder.Services.AddScoped<IJwtTokenService>(_ => new JwtTokenService(jwtSecret, jwtIssuer, jwtAudience));
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TrackFlow API",
        Version = "v1",
        Description = "Time Tracking API for Software Engineering Course",
        Contact = new OpenApiContact { Name = "Metodija Zdravkovski" }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TrackFlowDbContext>();
    await context.Database.MigrateAsync();
    await SeedData(context);
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrackFlow API v1"));
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

async Task SeedData(TrackFlowDbContext context)
{
    if (!await context.Users.AnyAsync())
    {
        var passwordHasher = new PasswordHasher();
        var admin = new TrackFlow.Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Email = "admin@trackflow.local",
            FirstName = "System",
            LastName = "Administrator",
            PasswordHash = passwordHasher.Hash("Admin123!"),
            Role = TrackFlow.Domain.Common.UserRole.Admin,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        context.Users.Add(admin);
        await context.SaveChangesAsync();
    }
}
