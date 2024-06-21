using PlaceRentalApp.API.Models;

var builder = WebApplication.CreateBuilder(args);

//Opção 1
//var min = builder.Configuration.GetValue<int>("PlacesConfig:MinDescription");
//var max = builder.Configuration.GetValue<int>("PlacesConfig:MaxDescription");

//var config = new PlacesConfiguration
//{
//    MinDescription = min,
//    MaxDescription = max,
//};

//Opção 2
//var config = new PlacesConfiguration();
//builder.Configuration.GetSection("PlacesConfig").Bind(config);
//builder.Services.AddSingleton(config);
//Opção 3
builder.Services.Configure<PlacesConfiguration>(
    builder.Configuration.GetSection("PlacesConfig"));


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
