using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using Scalar.AspNetCore;
using PokemonReviewApp.Mappers;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.repository;
using PokemonReviewApp.Models.Dto;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<PokemonMapper>();
builder.Services.AddScoped<OwnerMapper>();
builder.Services.AddScoped<ReviewMapper>();
builder.Services.AddScoped<CategoryMapper>();
builder.Services.AddScoped<CountryMapper>();
builder.Services.AddScoped<ReviewerMapper>();
builder.Services.AddScoped<CountryDto>();
builder.Services.AddScoped<OwnerDto>();
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();
builder.Services.AddScoped<Seed>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});



var app = builder.Build();

// Seed the database on startup (idempotent: guarded by !dataContext.PokemonOwners.Any())
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<Seed>().SeedDataContext();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
