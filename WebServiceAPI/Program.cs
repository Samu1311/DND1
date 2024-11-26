using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DND1.Data; // Namespace for your DbContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure secret key for JWT authentication
var key = Encoding.UTF8.GetBytes("a_super_secret_key_which_is_longer_than_32_characters");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourapp.com", // Replace with your actual issuer
            ValidAudience = "yourapp.com", // Replace with your actual audience
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Allow larger file uploads (optional, for image uploads)
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});

// Add database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5110") // Allow UI origin
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Enable Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger only in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Swagger available at root URL
    });
}

// Middleware pipeline
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(); // To serve files from wwwroot directory

app.MapControllers();

app.Run();
