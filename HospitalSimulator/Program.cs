using HospitalSimulatorConsole.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddTransient<IHospitalService, HospitalService>(); })
    .Build();

var hospital = host.Services.GetRequiredService<IHospitalService>();
await hospital.ExecuteAsync(args);

