using NordpoolUMMAppTask.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register the UMMApiService and configure HttpClient
builder.Services.AddHttpClient<UMMApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.MapControllers();

app.Run();