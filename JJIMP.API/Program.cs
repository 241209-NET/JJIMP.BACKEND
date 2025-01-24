using Microsoft.EntityFrameworkCore;
using JJIMP.API.Data;
using JJIMP.API.Service;
using JJIMP.API.Repository;

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

// Dependency Inject Repositories
// builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IIssueRepository, IssueRepository>();

// Add Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });

});

var app = builder.Build();

// This is a test comment from John
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
