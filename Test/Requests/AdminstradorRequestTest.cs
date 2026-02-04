        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Text;
        using System.Text.Json;
        using System.Threading.Tasks;
        using Microsoft.AspNetCore.ResponseCompression;
        using minimal_api.Dominio.ModelViews;
        using MinimalApi.DTOs;
        using Test.Helpers;
        using System.Net;

        namespace Test.Requests;

        [TestClass]
        public class AdministradorRequestTest
        {
            [ClassInitialize]
            public static  void ClassInit(TestContext testContext)
            {
                Setup.ClassInit(testContext);
            }
            [TestMethod]
            public async Task DeveRetornarAdministradorLogado_QuandoLoginForValido()
            {
                //Arrange (são as variaveis que são criadas para os testes)
                var loginDTO= new LoginDTO
                {
                    Email = "teste@teste.com",
                    Senha = "teste"
                };
                
                var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");
                //act (ação que serão executadas durante o processo)
                var response = await Setup.client.PostAsync("/Adminstradores/login", content);

                // assert (é a validação dos dados utilizados)
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                var result = await response.Content.ReadAsByteArrayAsync();

                var admLogado = JsonSerializer.Deserialize<AdministradorLogado>( result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

                Assert.IsNotNull(admLogado?.Email);
                Assert.IsNotNull(admLogado?.Perfil);
                Assert.IsNotNull(admLogado?.Token);

            }
        }

