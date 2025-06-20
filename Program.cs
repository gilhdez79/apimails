using ApiMails;
using ApiMails.interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient(
    "APIConnect",  //for demo only
    client =>
    {
        // Set the base address of the named client.
        client.BaseAddress = new Uri("https://www.google.com/");
    });
// Add services to the container.
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddCors();
builder.Services.AddCors(o => o.AddPolicy("CorePolicy", builder =>
{
    builder.AllowAnyMethod();
}));
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
