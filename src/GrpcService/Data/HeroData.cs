using GrpcService.Model;
using GrpcService.Protos;
using System.Text.Json;

namespace GrpcService.Data;

public class HeroData
{
    private const string HERO_JSON_FILE = "superheroes.json";
    private readonly IWebHostEnvironment _environment;

    public HeroData(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<IEnumerable<HeroResponse>> GetEnumerableAsync(CancellationToken token)
    {
        var filePath = Path.Combine(_environment.WebRootPath, HERO_JSON_FILE);
        var jsonString = await File.ReadAllTextAsync(filePath, token);
        var output = JsonSerializer.Deserialize<IEnumerable<SuperHeroResponse>>(jsonString);
        return output is not null
            ? output.Select(x => new HeroResponse()
            {
                Id= x.Id,
                Name= x.Name,
                Slug= x.Slug,
            })
            : Array.Empty<HeroResponse>();
    }

    public async Task<IEnumerable<SuperHeroResponseProto>> GetProtoEnumerableAsync(CancellationToken token)
    {
        var filePath = Path.Combine(_environment.WebRootPath, HERO_JSON_FILE);
        var jsonString = await File.ReadAllTextAsync(filePath, token);
        var output = JsonSerializer.Deserialize<IEnumerable<SuperHeroResponse>>(jsonString);
        return output is not null
            ? output.Select(x => Mapping.ToSuperHeroResponseProto(x))
            : Array.Empty<SuperHeroResponseProto>();
    }

    public async Task<HeroResponse?> GetByIdAsync(long Id, CancellationToken token)
    {
        var heroes = await GetEnumerableAsync(token);
        return heroes.FirstOrDefault(x => x.Id == Id);
    }
}
