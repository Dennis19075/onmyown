using Microsoft.Extensions.Configuration;
using Payable.API.Data;
using Payable.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//users
builder.Services.AddScoped<IUserContext, UsersContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Outcomes
builder.Services.AddScoped<IOutcomeContext, OutcomeContext>();
builder.Services.AddScoped<IOutcomeRepository, OutcomeRepository>();

//Incomes
builder.Services.AddScoped<IIncomeContext, IncomeContext>();
builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("http://localhost:8100").AllowAnyMethod().AllowAnyHeader();
}));

//enable single domain


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

