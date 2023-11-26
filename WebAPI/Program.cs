using System.Text;
using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using Domain.Auth;
using EfcDataAccess;
//using FileData;
//using FileData.DAOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//tell the framework is available for injection
//builder.Services.AddScoped<FileContext>();
//builder.Services.AddScoped<IUserDao, UserFileDao>();  //for file 
builder.Services.AddScoped<IUserDao, UserEfcDao>(); //for database 
builder.Services.AddScoped<IUserLogic, UserLogic>();

//add post -register services
//builder.Services.AddScoped<IProductDao, PostFileDao>(); //for file
builder.Services.AddScoped<IProductDao, ProductEfcDao>();  //for database 
builder.Services.AddScoped<IProductLogic, ProductLogic>();
//cat 
builder.Services.AddScoped<ICategoryDao, CategoryEfcDao>();  //for database 
builder.Services.AddScoped<ICategoryLogic, CategoryLogic>();

//sub cat 
builder.Services.AddScoped<ISubCategoryDao, SubCategoryEfcDao>();  //for database 
builder.Services.AddScoped<ISubCategoryLogic, SubCategoryLogic>();


//database 
builder.Services.AddDbContext<TGTGContext>();

// authentication with info about JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
//adding auth policies 
AuthorizationPolicies.AddPolicies(builder.Services);

var app = builder.Build();

//Blazor app to access the API
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

//Authentication 
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();