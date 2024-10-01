
using API.Middlewares;
using Domain.Common;
using Domain.Interfaces;
using Infrastructure.Services;
using Infrastructure.SQLServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

builder.Services.AddScoped< EmailService>();
builder.Services.AddScoped< TelegramService>();
//builder.Services.AddScoped<INotificationService, TelegramService>();
//builder.Services.AddScoped<INotificationService, TelegramService>(provider =>
//{
//    var options = provider.GetRequiredService<IOptions<AppSettingsOptions>>();
//    return new TelegramService(options);
//});
//builder.Services.AddScoped<INotificationService, EmailService>(provider => new EmailService());

builder.Services.AddScoped<INotificationFactory, NotificationFactory>();

//config DB connection
builder.Services.AddDbContext<SqlDBContext>(option =>
{
    var cnt = builder.Configuration.GetConnectionString("DBConnection");
    option.UseSqlServer(cnt);
});


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
        Description = "This is HealthEA API! ",
       
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
