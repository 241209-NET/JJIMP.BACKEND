
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using JJIMP.API.Data;
using JJIMP.API.Service;
using JJIMP.API.Repository;
using JJIMP.API.DTO;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add dbcontext and connect it to connection string
builder.Services.AddDbContext<JjimpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("JjimpDB")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Inject Services
// builder.Services.AddScoped<IService, Service>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserService, UserService>();

// Dependency Inject Repositories
// builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IIssueRepository, IssueRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });


builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        policy =>
        {
            policy
                .WithOrigins(
                    "http://localhost:5173"
                ) // specify your React dev origin
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials(); // allow credentials
        }
    );
});

//Add Service for Jwt Bearer Auth
builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            ),
        };
    });
    
var app = builder.Build();

// This is a test comment from John
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
