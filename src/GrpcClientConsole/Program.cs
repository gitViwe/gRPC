// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using GrpcClientConsole.Protos;

try
{
	var data = new HeroRequest { HeroId = 13 };
	var grpcChannel = GrpcChannel.ForAddress("https://localhost:7062", new GrpcChannelOptions()
	{
		ServiceConfig = new ServiceConfig()
		{
			MethodConfigs =
			{
				new MethodConfig()
				{
					Names = { MethodName.Default },
					RetryPolicy= new RetryPolicy()
					{
						MaxAttempts = 5,
						InitialBackoff= TimeSpan.FromSeconds(2),
						MaxBackoff = TimeSpan.FromSeconds(5),
						BackoffMultiplier = 1.5,
						RetryableStatusCodes = { StatusCode.Unknown, StatusCode.Unavailable }
					}
				}
			}

		}
	});
	var client = new Hero.HeroClient(grpcChannel);
	var response = await client.GetHeroByIDAsync(data);
	Console.WriteLine(response);
	Console.ReadLine();

	using (var clientData = client.GetHeroes(new HeroesRequest()))
	{
		while (await clientData.ResponseStream.MoveNext(CancellationToken.None))
		{
			Console.WriteLine(clientData.ResponseStream.Current);
		}
	}
	Console.ReadLine();

    using (var clientData = client.GetAll(new HeroesRequestProto()))
    {
        while (await clientData.ResponseStream.MoveNext(CancellationToken.None))
        {
            Console.WriteLine(clientData.ResponseStream.Current);
        }
    }
    Console.ReadLine();
}
catch (RpcException ex)
{
	Console.WriteLine(ex.Message);
}