using WorkerService1;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ScheduledWorker>();
builder.Services.AddSingleton<Start>();
builder.Services.AddSingleton<Validations>();
builder.Services.AddSingleton<Mp3Recover>();
builder.Services.AddSingleton<ValidateSize>();
builder.Services.AddSingleton<ValidateFormat>();
builder.Services.AddSingleton<SendToInvoxBatch>();


var host = builder.Build();
host.Run();