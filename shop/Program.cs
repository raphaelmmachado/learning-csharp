using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shop;
using Shop.Data;
// RUN
// dotnet dev-certs https --trust
// to trust SSL

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.

services.AddControllers();

//authentication
var key = Encoding.ASCII.GetBytes(Settings.Secret);
services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme =
     JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme =
     JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters =
     new TokenValidationParameters
     {
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(key),
         ValidateIssuer = false,
         ValidateAudience = false
     };
});


// In Memory - usar a memoria para testar api
// services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));

var connectionString = builder.Configuration
 .GetConnectionString("connectionString");
services.AddDbContext<DataContext>(
    opt => opt.UseSqlServer(connectionString)
);
services.AddScoped<DataContext, DataContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();

//add use authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
