using AutoTrackMinimalApi.Domain.DTOs;
using AutoTrackMinimalApi.Domain.DTOs.Enuns;
using AutoTrackMinimalApi.Domain.Entity;
using AutoTrackMinimalApi.Domain.Interfaces;
using AutoTrackMinimalApi.Domain.ModelViews;
using AutoTrackMinimalApi.Domain.Services;
using AutoTrackMinimalApi.Infrastructure.Database;
using AutoTrackMinimalApi.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;

#region Builder
var builder = WebApplication.CreateBuilder(args);

var key = builder.Configuration.GetSection("Jwt").GetValue<string>("Key");
if (string.IsNullOrEmpty(key) || key.Length < 16) key = "iRrGbPHhuGdoaPBBy1UvBv1nHgVRX9vHzvEP3ux+boM=";

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization();
builder.Services.AddScoped<IAdministratorServices, AdministratorServices>();
builder.Services.AddScoped<IVehiclesServices, VehiclesServices>();
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
        Description = "Insira seu token JWT aqui"
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
            new string[]{}
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();
#endregion

#region Home
app.MapGet("/", () => Results.Json(new Home())).AllowAnonymous().WithTags("Home");
#endregion

#region Administrator
string TokenGenerator(Administrator administrator)
{
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new List<Claim>()
    {
        new Claim("Email", administrator.Email),
        new Claim("Profile", administrator.Profile),
        new Claim(ClaimTypes.Role, administrator.Profile)
    };

    var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.Now.AddMinutes(30),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}

app.MapPost("administrator/login", ([FromBody] LoginDto loginDto, IAdministratorServices administratorServices) =>
{
    var adm = administratorServices.Login(loginDto);

    if (adm is not null)
    {
        var token = TokenGenerator(adm);
        return Results.Ok(new AdmLoggedInDto
        {
            Email = adm.Email,
            Profile = adm.Profile,
            Token = token
        });

    }
    return Results.Unauthorized();

}).AllowAnonymous().WithTags("Administrator");

app.MapGet("/administrators", ([FromQuery] int? page, IAdministratorServices administratorServices) =>

{
    var admins = new List<AdministratorDTO>();
    var administrator = administratorServices.GetAllAdmins(page);

    foreach (var admin in administrator)
    {
        admins.Add(new AdministratorDTO
        {
            Id = admin.Id,
            Email = admin.Email,
            Profile = admin.Profile
        });
    }
    return Results.Ok(admins);
}

).RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
.WithTags("Administrator");

app.MapGet("/administrator/{id}", ([FromRoute] int id, IAdministratorServices administratorServices) =>
{
    var administrator = administratorServices.GetAdminById(id);

    if (administrator is null) return Results.NotFound();

    var result = new AdministratorDTO
    {
        Email = administrator.Email,
        Profile = administrator.Profile
    };

    return Results.Ok(result);
})
.RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
.WithTags("Administrator");

app.MapPost("/administrator", ([FromBody] AdministratorCreateDTO administratorDto, IAdministratorServices administratorServices) =>
{
    var validators = new ErrosValidatorsMessage();

    if (string.IsNullOrEmpty(administratorDto.Email))
        validators.Messages.Add("Email nao pode ser vazio");

    if (string.IsNullOrEmpty(administratorDto.Password))
        validators.Messages.Add("Senha nao pode ser vazia");

    if (administratorDto.Profile == null)
        validators.Messages.Add("Perfil nao pode ser vazio");

    var administrator = new Administrator
    {
        Email = administratorDto.Email,
        Password = administratorDto.Password,
        Profile = administratorDto.Profile.ToString() ?? Profile.Editor.ToString()
    };

    var result = new AdministratorDTO
    {
        Email = administrator.Email,
        Profile = administrator.Profile.ToString()
    };

    administratorServices.Register(administrator);
    return Results.Created($"/administrator/{administrator.Id}", result);

})
.RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
.WithTags("Administrator");
#endregion

#region Vehicle
ErrosValidatorsMessage validatorDTO(VehicleDTO vehicleDto)
{
    var validators = new ErrosValidatorsMessage();

    if (string.IsNullOrEmpty(vehicleDto.Name))
        validators.Messages.Add("Nome não pode ser vazio");

    if (string.IsNullOrEmpty(vehicleDto.Model))
        validators.Messages.Add("Modelo não pode ser vazio");

    if (vehicleDto.YearFabrication < 1950)
        validators.Messages.Add("Veículo muito antigo para ser cadastrado");

    if (vehicleDto.YearFabrication > DateTime.Now.Year)
        validators.Messages.Add("Ano não pode ser maior que o ano atual");

    return validators;
}

app.MapPost("/veiculos", ([FromBody] VehicleDTO vehicleDto, IVehiclesServices vehiclesServices) =>
{

    var validators = validatorDTO(vehicleDto);
    if (validators.Messages.Count > 0)
        return Results.BadRequest(validators);

    var vehicle = new Vehicle()
    {
        Name = vehicleDto.Name,
        Model = vehicleDto.Model,
        YearFabrication = vehicleDto.YearFabrication
    };

    vehiclesServices.AddVehicle(vehicle);

    return Results.Created($"/veiculos/{vehicle.Id}", vehicle);
})
.RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
.RequireAuthorization()
.WithTags("Vehicles");

app.MapGet("/veiculos", ([FromQuery] int? page, IVehiclesServices vehiclesServices) =>
{
    var vehicles = vehiclesServices.GetAllVehicles(page);
    return Results.Ok(vehicles);
}).RequireAuthorization().WithTags("Vehicles");

app.MapGet("/veiculos/{id}", ([FromRoute] int id, IVehiclesServices vehiclesServices) =>
{
    var vehicle = vehiclesServices.GetVehicleById(id);

    if (vehicle is null) return Results.NotFound();

    return Results.Ok(vehicle);
}).RequireAuthorization().WithTags("Vehicles");

app.MapPut("/veiculos/{id}", ([FromRoute] int id, [FromBody] VehicleDTO vehicleDto, IVehiclesServices vehiclesServices) =>
{
    var vehicle = vehiclesServices.GetVehicleById(id);
    if (vehicle is null) return Results.NotFound();

    var validators = validatorDTO(vehicleDto);
    if (validators.Messages.Count > 0)
        return Results.BadRequest(validators);

    vehicle.Name = vehicleDto.Name;
    vehicle.Model = vehicleDto.Model;
    vehicle.YearFabrication = vehicleDto.YearFabrication;

    vehiclesServices.UpdateVehicle(vehicle);
    return Results.Ok(vehicle);
}).RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Admin, Editor" })
.WithTags("Vehicles");

app.MapDelete("/veiculos/{id}", ([FromRoute] int id, IVehiclesServices vehiclesServices) =>
{
    var vehicle = vehiclesServices.GetVehicleById(id);
    if (vehicle is null) return Results.NotFound();

    vehiclesServices.DeleteVehicle(vehicle);
    return Results.NoContent();
})
.RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
.WithTags("Vehicles");
#endregion


app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

