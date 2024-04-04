using intersectMessage.Data;
using intersectMessage.Data.Interfaces;
using intersectMessage.Data.Sevices;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mySqlConfign = new MySqlConfig(builder.Configuration.GetConnectionString("MySqlConnection"));
builder.Services.AddSingleton(mySqlConfign);

//builder.Services.AddSingleton(new MySqlConnection(builder.Configuration.GetConnectionString("MySqlConnection")));

builder.Services.AddScoped<IIntersectMessage, MessagesIntersetServices>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("policity", builder =>
    {
        builder.WithOrigins("https://d2ly8xc1lt88wm.cloudfront.net", "https://proyecto-c-sharp.vercel.app", "http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors("policity");

app.UseAuthorization();

app.MapControllers();

app.Run();
