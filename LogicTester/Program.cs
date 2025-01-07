using LogicTester.Models.Candel;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StockLoggerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StockLoggerDbContext")));

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // Allow localhost:3000 (your React app)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS globally or for specific controllers
app.UseCors("AllowLocalhost");  // This will enable CORS for the policy defined above

app.UseAuthorization();

app.MapControllers();

app.Run();
