using MobilApplikation.Data;
using MobilApplikation.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Allow requests from any origin (for development only)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowEmulator", policy =>
    {
        policy.AllowAnyOrigin()  // Android emulator acts like a separate origin
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? Configure DbContext with retry and proper SQL Server connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("DefaultConnection is not set in appsettings.json!");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        connectionString,
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowEmulator");
app.UseAuthorization();
app.MapControllers();

// ? Ensure database is created and seeded safely
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    try
    {
        // Force reset database for development
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        Console.WriteLine("Starting database seeding...");
        DbSeeder.Seed(context);
        Console.WriteLine("Database seeding completed.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error during database creation/seeding:");
        Console.WriteLine(ex);
        throw;
    }
}
app.Urls.Add("http://0.0.0.0:5000");

app.Run();
