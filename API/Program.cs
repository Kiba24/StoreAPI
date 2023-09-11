using API.Extension;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Initialize configuration
StartupExtension.Initialize(builder.Services, builder.Configuration);

//Rate limiting
StartupExtension.ConfigureRateLimiting(builder.Services);

var app = builder.Build();

//Initialize dataseed
StartupExtension.ConfigureSeed(app);



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.UseIpRateLimiting();

app.MapControllers();

app.Run();
