using EmployeeManagement.Models; // Make sure to use the correct namespace for your models
using EmployeeManagement.Models.Enums;
using EmployeeManagement.Services; // Make sure to import the namespace for your services (like IUserService, UserService, etc.)
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using EmployeeManagement.api.Infrastructure;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Swagger & API explorer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employee Management API",
        Version = "v1"
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    }
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {token}'"
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

// Step 1: Configure DbContext (SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Step 2: Add Identity (User and Role management)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Step 3: Add JWT Authentication
builder.Services.AddAuthentication("Bearer") // Use "Bearer" token authentication
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // In production, set this to true
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],    // Read from appsettings.json
            ValidAudience = builder.Configuration["Jwt:Audience"], // Read from appsettings.json
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])) // Secret key from appsettings.json
        };
    });

// Step 4: Register the Services in Dependency Injection (DI)
builder.Services.AddScoped<IUserService, UserService>();       // Register IUserService with UserService
builder.Services.AddScoped<IProjectService, ProjectService>(); // Register IProjectService with ProjectService
builder.Services.AddScoped<ITaskService, TaskService>();       // Register ITaskService with TaskService
builder.Services.AddScoped<IProfileService, ProfileService>(); // Register IProfileService with ProfileService

// Add controllers and other necessary services
builder.Services.AddControllers();

// CORS for local React app
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins(
                "http://localhost:5173",
                "http://localhost:3000"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

// Authorization policies using role constants
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole(Roles.Administrator));
    options.AddPolicy("EmployeeOrAdmin", policy => policy.RequireRole(Roles.Employee, Roles.Administrator));
});

var app = builder.Build();

// Swagger middleware (enable always for now; can restrict to Development later)
app.UseSwagger();
app.UseSwaggerUI();

// Step 5: Enable Authentication and Authorization in the middleware pipeline
app.UseCors("Frontend");
app.UseAuthentication();  // Enable JWT Authentication
app.UseAuthorization();   // Enable Authorization (checking roles, etc.)

app.MapControllers(); // Map controllers to routes

// Seed roles at startup (idempotent)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    await IdentitySeeder.SeedRolesAsync(roleManager);
    await IdentitySeeder.SeedAdminUserAsync(userManager, configuration);
}

app.Run();
