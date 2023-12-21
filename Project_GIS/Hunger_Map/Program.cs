using Hunger_Map.auth_settings;
using Hunger_Map.Business.Abstract;
using Hunger_Map.Business.Concret;
using Hunger_Map.Context;
using Hunger_Map.Repository.Abstract;
using Hunger_Map.Repository.Concret;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static Hunger_Map.Business.Abstract.IBaseBusiness;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HungerContext>(options =>
                           options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConectionString")));

builder.Services.AddHttpClient<IUserBusiness, UserBusiness>(s => s.BaseAddress =
               new Uri(builder.Configuration["ServiceUrls:Gis_Login"]));

builder.Services.AddScoped(typeof(IBaseBusiness<>), typeof(BaseBusiness<>));

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<IAddressBusiness, AddressBusiness>();




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var key = Encoding.ASCII.GetBytes(settings.secret);


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(x =>
    {

        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,

            //ValidIssuer = "https://localhost:5130",
            //ValidAudience = "https://localhost:5130",

        };
    });



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());


app.UseHttpsRedirection();


app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
