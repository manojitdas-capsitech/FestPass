using backend.Data;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Mongo Context
builder.Services.AddSingleton<MongoContext>();

builder.Services.AddScoped<ScanService>();
builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<FestService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Create Mongo Indexes at startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MongoContext>();
    MongoIndexes.CreateIndexes(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
