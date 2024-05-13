using dbf_api;

var builder = WebApplication.CreateBuilder(args);

if(Environment.GetEnvironmentVariable("API_KEY") is not {} apiKey)
{
    throw new Exception("API_KEY environment variable not set");
}


// Add services to the container.
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

app.UseHttpsRedirection();

app.UseApiKeyMiddleware("X-Api-Key", apiKey);

app.MapApiEndpoints();

app.Run();
