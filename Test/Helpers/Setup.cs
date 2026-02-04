using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MinimalApi.Dominio.interfaces;
using Test.Mocks;

namespace Test.Helpers;

public class Setup
{
    public static WebApplicationFactory<Startup> http = default!;
    public static HttpClient client = default!;

    public static void ClassInit(TestContext context)
    {
        http = new WebApplicationFactory<Startup>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");

                builder.ConfigureServices(services =>
                {
                    // 🔐 AUTH FAKE COMO DEFAULT
                    services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = "Test";
                        options.DefaultChallengeScheme = "Test";
                    })
                    .AddScheme<AuthenticationSchemeOptions, FakeAuthHandlerVeiculos>(
                        "Test", options => { });

                    services.AddAuthorization();

                    // remove serviços reais
                    services.RemoveAll<IAdministradorServico>();
                    services.RemoveAll<IVeiculoServico>();

                    // injeta mocks
                    services.AddScoped<IAdministradorServico, AdministradorServicoMock>();
                    services.AddScoped<IVeiculoServico, VeiculoServicoMock>();
                });
            });

        client = http.CreateClient();

        // 🧪 HEADER DE AUTH (OBRIGATÓRIO)
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Test");
    }

    public static void ClassCleanup()
    {
        http.Dispose();
    }
}
