using NetCoreX.API.Configurations;
using NetCoreX.Data.Queries;
using NetCoreX.Data.Repositories;
using NetCoreX.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddAppConfigurationConfiguration(builder.Environment);
builder.Services.AddAppConfigurationConfiguration(builder.Environment);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Queries).Assembly);
});
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddExceptionConfiguration();

builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddAuthorizationConfiguration();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.ApplyException(app.Environment);

app.UseHttpsRedirection();

app.ApplySwagger();

app.ApplyAppConfiguration(app.Environment);

app.ApplyAuth();

app.RegisterContactsEndpoints();

app.ApplyDatabaseSchema(app.Environment);

app.Run();