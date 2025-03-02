using SIITest.Core.Bussiness;
using SIITest.Core.Interfaces;
using SIITest.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


AddDependencyInjectionServices();

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy.WithOrigins("http://localhost:4200") 
                        .AllowAnyMethod() 
                        .AllowAnyHeader() 
                        .AllowCredentials()); 
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseCors("AllowAngular");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

void AddDependencyInjectionServices()
{
    builder.Services.AddHttpClient<IExternalService, ExternalService>();
    builder.Services.AddScoped<IProductsService, ProductsService>();
    builder.Services.AddScoped<IBusinessService, BusinessService>();
    builder.Services.AddScoped<ITaxCalculationStrategy, PercentageTaxCalculationStrategy>(); 
}
