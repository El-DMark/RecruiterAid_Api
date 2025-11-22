using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecruiterAid_Api.Application.Services;
using RecruiterAid_Api.Infrastructure.Data;
using RecruiterAid_Api.Infrastructure.Identity; // IdentitySeeder
using RecruiterAid_Api.Domain.Entities.Identity; // AppUser
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// ✅ Bind to Render's PORT (default 10000)
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Configure EF Core with PostgreSQL
var connectionString =
    builder.Configuration.GetConnectionString("PostgresConnection")
    ?? Environment.GetEnvironmentVariable("PostgresConnection");

Console.WriteLine($"🔎 Using Postgres connection string: {connectionString}");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configure Identity with AppUser
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Register application services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJobPostingService, JobPostingService>();
builder.Services.AddScoped<IEmployerService, EmployerService>();

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your JWT token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

// Configure JWT authentication
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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var identity = context.Principal.Identity as ClaimsIdentity;
            var customClaim = identity?.FindFirst(ClaimTypes.NameIdentifier);

            if (customClaim != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, customClaim.Value));
                Console.WriteLine($"✅ Mapped nameidentifier: {customClaim.Value}");
            }
            else
            {
                Console.WriteLine("❌ nameidentifier claim not found in token");
            }

            return Task.CompletedTask;
        }
    };
});

// ✅ Add Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanAssignCandidates", policy =>
        policy.RequireRole("Admin", "Manager"));

    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("ManagerOwnTeam", policy =>
        policy.RequireAssertion(context =>
        {
            var user = context.User;
            if (user.IsInRole("Admin"))
                return true;

            if (user.IsInRole("Manager"))
            {
                var managerIdClaim = user.FindFirst("ManagerId")?.Value;
                var routeManagerId = context.Resource as HttpContext
                    ?? throw new InvalidOperationException("No HttpContext in resource");

                var requestedManagerId = routeManagerId.Request.RouteValues["managerId"]?.ToString();
                return managerIdClaim != null && managerIdClaim == requestedManagerId;
            }

            return false;
        }));
});

// Optional: Enable CORS for frontend integration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Swagger UI for development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ❌ Remove HTTPS redirection for Render (proxy handles HTTPS)
// app.UseHttpsRedirection();

app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// ✅ Add a simple health endpoint for Render checks
app.MapGet("/health", () => Results.Ok("Healthy"));

// ✅ Apply migrations and seed roles/users on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();

    try
    {
        Console.WriteLine("📦 Applying migrations...");
        dbContext.Database.Migrate();
        Console.WriteLine("✅ Migrations applied successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️ Migration failed: {ex.Message}");
    }

    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await IdentitySeeder.SeedAsync(userManager, roleManager, dbContext);
}

app.Run();
