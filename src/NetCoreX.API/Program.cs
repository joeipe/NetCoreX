using NetCoreX.API.Configurations;
using NetCoreX.Data.Queries;
using NetCoreX.Data.Repositories;
using NetCoreX.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Queries).Assembly);
});
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseHttpsRedirection();

app.ApplySwagger();

app.RegisterContactsEndpoints();

app.ApplyDatabaseSchema(app.Environment);

app.Run();