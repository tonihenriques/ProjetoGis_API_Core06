using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_GIS_Login.auth_settings;
using Project_GIS_Login.Business.Abstract;
using Project_GIS_Login.Business.Concrect;
using Project_GIS_Login.Context;
using Project_GIS_Login.Repository.Abstract;
using Project_GIS_Login.Repository.Concrect;
using System.Text;
using static Project_GIS_Login.Business.Abstract.IBaseBusiness;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LoginContext>(options =>
                           options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConectionString")));

builder.Services.AddScoped(typeof(IBaseBusiness<>), typeof(BaseBusiness<>));

builder.Services.AddScoped<IUserBusiness, UserBusiness>();

builder.Services.AddScoped<ILoginBusiness, LoginBusiness>();

builder.Services.AddScoped<IRolesBusiness, RoleBusiness>();

builder.Services.AddScoped<IUserRolesBusiness, UserRolesBusiness>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));


// Add services to the container.

builder.Services.AddControllers();
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
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,

            ValidIssuer = "http://localhost:5130",
            ValidAudience = "http://localhost:5130",


        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
