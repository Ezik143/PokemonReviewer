using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PokemonReviewAPI.Data;
using Scalar.AspNetCore;
using PokemonReviewAPI.Mappers;
using PokemonReviewAPI.interfaces;
using PokemonReviewAPI.repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FlutterDevelopment", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.AddScoped<PokemonMapper>();
builder.Services.AddScoped<OwnerMapper>();
builder.Services.AddScoped<ReviewMapper>();
builder.Services.AddScoped<CategoryMapper>();
builder.Services.AddScoped<CountryMapper>();
builder.Services.AddScoped<ReviewerMapper>();
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();
builder.Services.AddScoped<Seed>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, _, _) =>
    {
        document.Info = new OpenApiInfo
        {
            Title = "Pokemon Review API",
            Version = "v1",
            Description = "REST API for managing Pokémon, owners, reviewers, categories, and reviews."
        };

        return Task.CompletedTask;
    });
});
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
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Pokemon Review API")
            .WithTheme(ScalarTheme.Purple)
            .EnableDarkMode()
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
            .WithDocumentDownloadType(DocumentDownloadType.Both);
    });
    app.UseCors("FlutterDevelopment");
}

// The Flutter development client uses the HTTP launch profile. Production
// deployments should use HTTPS and keep this redirect enabled there.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
