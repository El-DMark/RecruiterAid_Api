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

// Configure EF Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity with AppUser
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Register application services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Enable JWT authentication in Swagger
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
    // Admins and Managers can assign candidates
    options.AddPolicy("CanAssignCandidates", policy =>
        policy.RequireRole("ADMIN", "MANAGER"));

    // Managers can only access their own team; Admins can access any
    options.AddPolicy("ManagerOwnTeam", policy =>
        policy.RequireAssertion(context =>
        {
            var user = context.User;
            if (user.IsInRole("ADMIN"))
                return true;

            if (user.IsInRole("MANAGER"))
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

// Middleware pipeline
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Seed roles and default users on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var dbContext = services.GetRequiredService<ApplicationDbContext>();

    await IdentitySeeder.SeedAsync(userManager, roleManager, dbContext);
}


app.Run();
