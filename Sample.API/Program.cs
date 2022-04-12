using Sample.API.Clients;
using Sample.API.Repositories;
using Sample.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddHealthChecks();

builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddTransient<ISWAPIClient, SWAPIClient>();
builder.Services.AddHttpClient<SWAPIClient>();
builder.Services.AddTransient<ISWAPIPeopleClient, SWAPIPeopleClient>();
builder.Services.AddScoped<ISwapiPeopleService, SwapiPeopleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();

app.UseHttpLogging();
app.UseHealthChecks("/hc");

app.Run();
