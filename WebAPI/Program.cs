using Business.DependencyResolver;
using Core.DependencyResolver;
using FluentValidation.AspNetCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBusinessService();
builder.Services.AddCoreService();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region FluentValidation
FluentValidationMvcExtensions.AddFluentValidation(builder.Services.AddControllersWithViews(), x =>
{
    x.RegisterValidatorsFromAssemblyContaining<Program>();
    x.ValidatorOptions.LanguageManager.Culture = new CultureInfo("ru-RU");
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
