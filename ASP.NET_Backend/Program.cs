using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Data Source=103.109.37.107,5002;Initial Catalog=bonsaiShop;Persist Security Info=True;User ID=SA;Password=yourStrongPassword@#;Trust Server Certificate=True"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "bao",
                        ValidAudience = "baobao",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MY_SECRET_KEY_IS_LONG_ENOUGH_12345"))
                    };
                });

builder.Services.AddMvc();
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
