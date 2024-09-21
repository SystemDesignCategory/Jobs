using Hangfire;
using VideoConverter;
using VideoConverter.Jobs;
using VideoConverter.VideoConverter;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<VideoConverterService>();
builder.Services.AddHostedService<RegisterJobsHostService>();

builder.Services.Register(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
});

app.UseHangfireDashboard();
app.EndPoints();

app.Run();