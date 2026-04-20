using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<TeamDbContextDAO>();
builder.Services.AddScoped<FoodDbContextDAO>();
builder.Services.AddScoped<HobbyDbContextDAO>();
builder.Services.AddScoped<MovieDbContextDAO>();
builder.Services.AddScoped<ITeamDbContextDAO>(sp => sp.GetRequiredService<TeamDbContextDAO>());
builder.Services.AddScoped<IFoodDbContextDAO>(sp => sp.GetRequiredService<FoodDbContextDAO>());
builder.Services.AddScoped<IHobbyDbContextDAO>(sp => sp.GetRequiredService<HobbyDbContextDAO>());
builder.Services.AddScoped<IMovieDbContextDAO>(sp => sp.GetRequiredService<MovieDbContextDAO>());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerDocument();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi();
}

//Sami Cox - migrate DB on startup
using (var dbScope = app.Services.CreateScope())
{
    var appDB = dbScope.ServiceProvider.GetRequiredService<AppDbContext>();
    appDB.Database.Migrate();
}

app.UseOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
