using Hangfire;

using HangfireDemo.Services;

using System.Runtime.ConstrainedExecution;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //Add Hangfire
    builder.Services.AddHangfire(config =>
    {
        //config.UseSqlServerStorage("<connection string>");
        config.UseInMemoryStorage();
    });
    builder.Services.AddHangfireServer(options =>
    {
        //Default: 5 worker/Processor Count
        //Math.Min(Environment.ProcessorCount * 5, 20);
        //options.WorkerCount = 5;
    });

    //Register services
    builder.Services.AddScoped<IJobTestService, JobTestService>();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseHangfireDashboard();

    app.Run();
}