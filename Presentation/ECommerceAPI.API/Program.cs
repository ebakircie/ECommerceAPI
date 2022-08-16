using ECommerceAPI.Application.Validators.Products;
using ECommerceAPI.Infrastructure.Filters;
using ECommerceAPI.Persistence;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();

builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=> // NOTE: CORS politikalar� (detay gitbook'da) 
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()    
));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration=> configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options=>options.SuppressModelStateInvalidFilter=true); // Default filtreleri kald�rmazsak, istek controller'a gelmeden do�rulama yap�l�yor ve client'a geri d�nd�r�yor. Devre d��� kald���nda, model.state'e istek giricek ve sonucu g�rebilece�iz.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(); // yukarda belirledi�imiz politikay�, middleware'de �a��rmam�z gerekiyor.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
