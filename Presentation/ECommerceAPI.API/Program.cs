using ECommerceAPI.Application.Validators.Products;
using ECommerceAPI.Infrastructure.Filters;
using ECommerceAPI.Persistence;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();

builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=> // NOTE: CORS politikalarý (detay gitbook'da) 
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()    
));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration=> configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options=>options.SuppressModelStateInvalidFilter=true); // Default filtreleri kaldýrmazsak, istek controller'a gelmeden doðrulama yapýlýyor ve client'a geri döndürüyor. Devre dýþý kaldýðýnda, model.state'e istek giricek ve sonucu görebileceðiz.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(); // yukarda belirlediðimiz politikayý, middleware'de çaðýrmamýz gerekiyor.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
