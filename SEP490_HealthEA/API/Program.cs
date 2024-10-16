
using API.Middlewares;
using Domain.Interfaces.IRepositories;
using Domain.Interfaces.IServices;
using Domain.Services;
using Infrastructure.Repositories;
using Domain.Common;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.SQLServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Read JWT Key from appsetting
var jwtSettings = builder.Configuration.GetSection("Jwt");

// Add full cors 
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*")
                          .AllowAnyHeader()
                           .AllowAnyMethod();
                      });
});

// Implement JWT services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
#pragma warning disable CS8604 // Possible null reference argument.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };
#pragma warning restore CS8604 // Possible null reference argument.

        // Cấu hình để Swagger có thể sử dụng JWT token từ Authorization header mà không cần tiền tố 'Bearer '
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Lấy token từ query string nếu có
                if (context.Request.Query.ContainsKey("access_token"))
                {
                    context.Token = context.Request.Query["access_token"];
                }
                return Task.CompletedTask;
            }
        };
    });
//

builder.Services.Configure<AppSettingsOptions>(builder.Configuration.GetSection("TelegramSettings"));

builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<TelegramService>();

builder.Services.AddSingleton<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddHttpClient();

//config DB connection
builder.Services.AddDbContext<SqlDBContext>(option =>
{
    var cnt = builder.Configuration.GetConnectionString("DBConnection");
    option.UseSqlServer(cnt);
});
//config service
builder.Services.AddScoped<IMedicalRecordsService, MedicalRecordsService>();
//config repo
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
//another
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

//config middleware Global Exception
builder.Services.AddExceptionHandler<GlobalExceptionHandlingMiddleware>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Setup header Swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "HealthEA API for Group 1",
        Description = "This is Token user: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiSmFuZVNtaXRoIiwianRpIjoiNWVmZDdjOWItNDI5NS00NjM4LThiOWYtMmQwY2ZlMTI4NjY0IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImV4cCI6MjMyODQxNjA4OSwiaXNzIjoiUTEiLCJhdWQiOiJRMSJ9.LtsIsOniGTKAP1Lv_s0IpmRBot8XeOVzon4gI8KanTc ",
       
    });

    // Setup JWT helper for Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        //option.RoutePrefix = "docs";
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "First version API");
    });
}

//app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.Run();
