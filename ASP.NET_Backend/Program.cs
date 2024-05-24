using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Data Source=103.109.37.107,5002;Initial Catalog=bonsaiShop;Persist Security Info=True;User ID=SA;Password=yourStrongPassword@#;Trust Server Certificate=True"));


var app = builder.Build();

app.MapControllers();

app.Run();
