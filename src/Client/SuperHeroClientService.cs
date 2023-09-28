using Shared.Protos;

namespace Client;

internal class SuperHeroClientService
{
    private readonly SuperHero.SuperHeroClient _client;

    public SuperHeroClientService(SuperHero.SuperHeroClient client)
    {
        _client = client;
    }

    public async Task StreamSuperHeroAsync()
    {
        using (var response = _client.Get(new SuperHeroRequestProto()))
        {
            while (await response.ResponseStream.MoveNext(CancellationToken.None))
            {
                Console.WriteLine(response.ResponseStream.Current.Name);
            }
        }
        Console.ReadLine();
    }

    public void SuperHeroById(SuperHeroByIdRequestProto request)
    {
        var response = _client.GetById(request);
        Console.WriteLine(response);
        Console.WriteLine("============================");
        Console.WriteLine("Up next we will stream data, press any key to continue!");
        Console.WriteLine("============================");
        Console.ReadLine();
    }
}
