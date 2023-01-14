using Client;
using Client.Extension;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("This is a gRPC demo client console!");

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.RegisterGrpcClient();
        services.AddSingleton<SuperHeroClientService>();
    })
    .Build();

using IServiceScope serviceScope = host.Services.CreateScope();
var clientService = serviceScope.ServiceProvider.GetRequiredService<SuperHeroClientService>();

clientService.SuperHeroById(new Shared.Protos.SuperHeroByIdRequestProto() { Id = 1 });
await clientService.StreamSuperHeroAsync();
