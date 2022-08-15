using ECommerceAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();

builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=> // NOTE: CORS politikaları (detay gitbook'da) 
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()    
));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(); // yukarda belirlediğimiz politikayı, middleware'de çağırmamız gerekiyor.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
