using Microsoft.EntityFrameworkCore; // Pentru AddDbContext și UseSqlServer
using SimpleBlog.Database;          // Pentru a accesa BlogDbContext
using SimpleBlog.Core.Interfaces;     // Pentru IUserRepository
using SimpleBlog.Database.Repositories; // Pentru UserRepository
using SimpleBlog.Core.Services;   // Pentru UserService
using SimpleBlog.Api.Middleware; // Pentru ErrorHandlingMiddleware

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build(); // Aplicația este construită aici

// --- Seed the database ---
try
{
    // Folosim metoda de extensie creată
    await app.Services.SeedDatabaseAsync();
}
catch (Exception ex)
{
    // Loghează eroarea (într-o aplicație reală ai folosi un logger configurat)
    Console.WriteLine($"An error occurred seeding the DB: {ex.Message}");
}
// --- End Seeding ---

// Trebuie să fie printre primele în pipeline pentru a prinde erorile
// din middleware-urile și endpoint-urile ulterioare.
app.UseMiddleware<ErrorHandlingMiddleware>();
// --- Sfârșit adăugare Middleware ---


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();