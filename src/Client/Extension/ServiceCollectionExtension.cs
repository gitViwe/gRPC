using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Protos;

namespace Client.Extension;

internal static class ServiceCollectionExtension
{
    public static IHttpClientBuilder RegisterGrpcClient(this IServiceCollection services)
    {
        return services.AddGrpcClient<SuperHero.SuperHeroClient>(options =>
        {
            options.Address = new Uri("http://localhost:5007");
        })
        .ConfigureChannel(options =>
        {
            options.ServiceConfig = new ServiceConfig()
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

            };
        })
        .EnableCallContextPropagation(options =>
        {
            options.SuppressContextNotFoundErrors = true;
        });
    }
}
