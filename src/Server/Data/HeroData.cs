using Shared.Model;
using Shared.Protos;
using System.Text.Json;

namespace Server.Data;

public class HeroData
{
    private const string HERO_JSON_FILE = "superheroes.json";
    private readonly IWebHostEnvironment _environment;

    public HeroData(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    private async Task<IEnumerable<SuperHeroResponse>> GetEnumerableAsync(CancellationToken token = default)
    {
        var filePath = Path.Combine(_environment.WebRootPath, HERO_JSON_FILE);
        var jsonString = await File.ReadAllTextAsync(filePath, token);
        return JsonSerializer.Deserialize<IEnumerable<SuperHeroResponse>>(jsonString)
            ?? Enumerable.Empty<SuperHeroResponse>();
    }
    public async Task<IEnumerable<SuperHeroResponseProto>> GetProtoEnumerableAsync(CancellationToken token)
    {
        var output = await GetEnumerableAsync(token);
        return output.Any()
            ? output.Select(Mapping.ToSuperHeroResponseProto)
            : Array.Empty<SuperHeroResponseProto>();
    }

    public async Task<SuperHeroResponseProto> GetProtoByIdAsync(long Id, CancellationToken token)
    {
        var heroes = await GetEnumerableAsync(token);
        var hero = heroes.FirstOrDefault(x => x.Id == Id);
        return hero is not null
            ? Mapping.ToSuperHeroResponseProto(hero)
            : new SuperHeroResponseProto();
    }
}
