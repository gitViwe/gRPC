using Grpc.Core;
using Server.Data;
using Shared.Protos;

namespace Server.Services
{
    public class SuperHeroService : SuperHero.SuperHeroBase
    {
        private readonly HeroData _heroData;

        public SuperHeroService(HeroData heroData)
        {
            _heroData = heroData;
        }

        public override async Task<SuperHeroResponseProto> GetById(SuperHeroByIdRequestProto request, ServerCallContext context)
        {
            return await _heroData.GetProtoByIdAsync(request.Id, context.CancellationToken);
        }

        public override async Task Get(SuperHeroRequestProto request, IServerStreamWriter<SuperHeroResponseProto> responseStream, ServerCallContext context)
        {
            var heroes = await _heroData.GetProtoEnumerableAsync(context.CancellationToken);
            foreach (var hero in heroes)
            {
                await Task.Delay(500);
                await responseStream.WriteAsync(hero);
            }
        }
    }
}
