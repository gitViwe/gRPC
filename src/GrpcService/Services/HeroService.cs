using Grpc.Core;
using GrpcService.Data;
using GrpcService.Protos;

namespace GrpcService.Services;

public class HeroService : Hero.HeroBase
{
    private readonly ILogger<HeroService> _logger;
    private readonly HeroData _data;

    public HeroService(ILogger<HeroService> logger, HeroData data)
	{
        _logger = logger;
        _data = data;
    }

    public override async Task<HeroResponse> GetHeroByID(HeroRequest request, ServerCallContext context)
    {
        var response = await _data.GetByIdAsync(request.HeroId, context.CancellationToken);
        return response ?? new HeroResponse();
    }

    public override async Task GetHeroes(HeroesRequest request, IServerStreamWriter<HeroResponse> responseStream, ServerCallContext context)
    {
        var heroes = await _data.GetEnumerableAsync(context.CancellationToken);
        foreach (var hero in heroes)
        {
            await Task.Delay(1000);
            await responseStream.WriteAsync(hero);
        }
    }

    public override async Task GetAll(HeroesRequestProto request, IServerStreamWriter<SuperHeroResponseProto> responseStream, ServerCallContext context)
    {
        var heroes = await _data.GetProtoEnumerableAsync(context.CancellationToken);
        foreach (var hero in heroes)
        {
            await Task.Delay(1000);
            await responseStream.WriteAsync(hero);
        }
    }
}
