using CallServer.Data;
using CallServer.Hubs;
using CallServer.Repositories;
using CallServer.Services;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CallTrackingDbContext>();
builder.Services.AddHttpContextAccessor();

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

app.UseExceptionHandler(options =>
{
    options.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var exception = context.Features.Get<IExceptionHandlerFeature>();
        if(exception != null)
        {
            await context.Response.WriteAsync(exception.Error.Message);
        }
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("ClientPermission");

app.MapControllers();

app.MapHub<CallHub>("/hub/call");
app.MapHub<Dashboard>("/hub/dashboard");

app.Run();
