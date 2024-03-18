using CallServer.Data;
using CallServer.Hubs;
using CallServer.Repositories;
using CallServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CallTrackingDbContext>();

builder.Services.AddScoped<IAgentRepository, AgentRepository>();
builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<ICallDetailRepository, CallDetailRepository>();
builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<ICallDetailService, CallDetailService>();
builder.Services.AddScoped<IHospitalService, HospitalService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientPermission", policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:5000")
        .AllowCredentials();
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

app.UseAuthorization();
app.UseCors("ClientPermission");

app.MapControllers();
app.MapHub<Hospital_1>("/hub/call/1");
app.MapHub<Hospital_2>("/hub/call/2");
app.MapHub<Dashboard>("/hub/dashboard");
app.Run();
