﻿
using API.Middlewares;
using Domain.Interfaces;
using Domain.Interfaces.IRepositories;
using Domain.Interfaces.IServices;
using Domain.Mappings;
using Domain.Services;
using Infrastructure.MediatR.Events.Commands.CreateEvent;
using Infrastructure.Notification;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.Background;
using Infrastructure.Services.Ocr;
using Infrastructure.SQLServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using System.Reflection;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Read JWT Key from appsetting
var jwtSettings = builder.Configuration.GetSection("Jwt");
var projectId = "sep490-c3c0a";

// Đăng ký FirebaseNotificationService với HttpClient và projectId
//builder.Services.AddHttpClient<FirebaseNotificationService>(client =>
//{
//    client.BaseAddress = new Uri("https://fcm.googleapis.com/");
//})
//.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler())
//.AddSingleton(sp => new FirebaseNotificationService(sp.GetRequiredService<HttpClient>(), projectId));

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
//config DB connection
builder.Services.AddDbContext<SqlDBContext>(option =>
{
    var cnt = builder.Configuration.GetConnectionString("DBConnection");
    option.UseSqlServer(cnt);
});
builder.Services.AddScoped<ReminderService>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<EmailService>();


//builder.Services.AddQuartz(q =>
//{
//    q.UseMicrosoftDependencyInjectionJobFactory();

//    var jobKey = new JobKey("reminderJob");
//    q.AddJob<ReminderJob>(opts => opts.WithIdentity(jobKey));

//    // Tạo scope để lấy dữ liệu từ ReminderService
//    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
//    {
//        var reminderService = scope.ServiceProvider.GetRequiredService<ReminderService>();
//        var reminderTimes = reminderService.GetReminderTimes();

//        foreach (var reminderTime in reminderTimes)
//        {
//            // Tạo cron expression từ giờ và phút trong reminderTime
//            string cronExpression = $"0 {reminderTime.Minute} {reminderTime.Hour} * * ?";

//            // Tạo trigger cho từng ReminderTime
//            q.AddTrigger(opts => opts
//                .ForJob(jobKey)
//                .WithIdentity($"reminderTrigger-{reminderTime:HHmm}")
//                .UsingJobData("reminderTime", reminderTime.ToString("HH:mm", CultureInfo.InvariantCulture))
//                .WithCronSchedule(cronExpression));
//        }

//    }
//});
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();  

    var jobKey = new JobKey("reminderJob");
    q.AddJob<ReminderJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts =>
    {
        opts
            .ForJob(jobKey)
            .WithIdentity("reminderTrigger")
            .WithCronSchedule("0/5 * * * * ?");
    });
});


//builder.Services.AddQuartz(q =>
//{
//    q.UseMicrosoftDependencyInjectionJobFactory();  // Dùng DI container của Microsoft cho Quartz

//    // Đăng ký ReminderJob nếu bạn sử dụng QuartzJob
//    q.AddJob<ReminderJob>(opts => opts.WithIdentity("reminderJob"));
//});

//builder.Services.AddQuartzHostedService(options =>
//{
//    options.WaitForJobsToComplete = true;  // Đảm bảo Quartz chờ các job hoàn thành khi dừng
//});


builder.Services.AddSingleton<IHostedService, QuartzHostedService>();

//config service
builder.Services.AddSingleton<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IMedicalRecordsService, MedicalRecordsService>();
builder.Services.AddScoped<IOcrService, OcrService>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
//config repo
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordsRepository>();
//builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserReportRepository, UserReportRepository>();
builder.Services.AddScoped<IImageScanService, ImageScanService>();
builder.Services.AddScoped<IOpenAIChatService, OpenAIChatService>();
builder.Services.AddScoped<INoticeService, NoticeService>();
//builder.Services.AddMediatR(typeof(CreateNoticeCommandHandler).Assembly);
builder.Services.AddScoped<FirebaseNotificationService>();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    Assembly.GetExecutingAssembly(),
    typeof(CreateEventWithUserCommandHandler).Assembly
));

string serviceAccountFilePath = Path.Combine(builder.Environment.ContentRootPath, "firebaseServiceAccount.json");

builder.Services.AddHttpClient<FirebaseNotificationService>();
builder.Services.AddScoped<FirebaseNotificationService>(provider =>
{
    var httpClient = provider.GetRequiredService<HttpClient>();
    var firebaseSettings = provider.GetRequiredService<IOptions<FirebaseSettings>>();
    var context = provider.GetRequiredService<SqlDBContext>();
    return new FirebaseNotificationService(httpClient, firebaseSettings, context);
});




builder.Services.AddScoped<IDailyMetricRepository, DailyMetricRepository>();
builder.Services.AddScoped<IDailyMetricsAnalysisService, DailyMetricAnalysisService>();
builder.Services.AddScoped<IUserClaimsService, UserClaimsService>();
builder.Services.AddScoped<IDoctorRepository,DoctorRepository>();
//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
//Background Service
builder.Services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
builder.Services.AddHostedService<HeavyTaskBackgroundService>();
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
        Description = "This is Token user: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcm5hbWUiLCJqdGkiOiJlOTFiZDczZS0xOGUwLTQ1ZWQtYjA5ZS00ZThlZDA2YjNlNzgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDVVNUT01FUiIsImV4cCI6MjMyOTQyODA3MSwiaXNzIjoiZHVvbmcxMi5jb20iLCJhdWQiOiJkdW9uZzEyLmNvbSJ9.Bl-iKNtu8RYQBVzRX5tQnzA5CHZN8SQsuWds-FDg5BI",

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
