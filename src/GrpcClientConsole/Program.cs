// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using GrpcClientConsole.Protos;

Console.WriteLine("Hello, World!");

try
{
	var data = new HeroRequest { HeroId = 13 };
	var grpcChannel = GrpcChannel.ForAddress("https://localhost:7062");
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
}
catch (RpcException ex)
{

	throw;
}