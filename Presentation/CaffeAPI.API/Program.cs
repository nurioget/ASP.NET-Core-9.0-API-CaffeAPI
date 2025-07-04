using CaffeAPI.Aplication.Dtos.CategoryDtos;
using CaffeAPI.Aplication.Dtos.MenuItemDtos;
using CaffeAPI.Aplication.Dtos.OrderDtos;
using CaffeAPI.Aplication.Dtos.OrderItemDtos;
using CaffeAPI.Aplication.Dtos.TablesDtos;
using CaffeAPI.Aplication.Helpers;
using CaffeAPI.Aplication.Interfaces;
using CaffeAPI.Aplication.Mapping;
using CaffeAPI.Aplication.Services.Abstract;
using CaffeAPI.Aplication.Services.Concrete;
using CaffeAPI.Persistence.Context;
using CaffeAPI.Persistence.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var conf = builder.Configuration;
    var database = conf.GetConnectionString("DefaultConnection");
    opt.UseSqlServer(database);
});


builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IMenuItemServices, MenuItemServices>();
builder.Services.AddScoped<ITableServices, TableServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IOrderItemServices, OrderItemServices>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<TokenHelpers>();

builder.Services.AddAutoMapper(typeof(GeneralMapping));

builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryDto>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateMenuItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateMenuItemDto>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateTableDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateTableDto>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateOrderDto>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateOrderItemDto>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

builder.Services.AddEndpointsApiExplorer();

app.MapScalarApiReference(
    opt =>
    {
        opt.Title = "Caffe API V1";
        opt.Theme = ScalarTheme.BluePlanet;
        opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
    });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
