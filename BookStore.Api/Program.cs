using BookStore.Api.Data;
using BookStore.Api.Models;
using BookStore.Api.Repositry;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

//Database connection service (With Connection String "BookStoreDB" from appsetting json file)
builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BookStoreDB")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>().AddDefaultTokenProviders();



//Controllers Service
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Dependency Injection Service
builder.Services.AddTransient<IBookRepositry, BookRepositry>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
//Auto Mapper Service
builder.Services.AddAutoMapper(typeof(Program));
// Service to fix CORS error
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Allow the CORS service to run in the web api
app.UseCors();

//Allows authorization of methods & resouces in controllers
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
