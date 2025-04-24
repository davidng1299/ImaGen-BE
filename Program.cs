using Microsoft.EntityFrameworkCore;
using ImaGen_BE.Models;
using ImaGen_BE.Services;
using Amazon.S3;
using Amazon;
using DotNetEnv;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Env.Load();
}

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables();

// Set up database connection
builder.Services.AddDbContext<DataContext>(otp => otp.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Set up AWS connection
builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var config = builder.Configuration.GetSection("AWS");
    return new AmazonS3Client(
        config["AccessKey"],
        config["SecretKey"],
        RegionEndpoint.GetBySystemName(config["Region"]!)
    );
});

// set up API configurations
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IOAImageService, OAImageService>();
builder.Services.AddScoped<S3Service>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
