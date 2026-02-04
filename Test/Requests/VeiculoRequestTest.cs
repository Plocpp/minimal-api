using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinimalApi.DTOs;
using MinimalApi.Dominio.Entidades;
using Test.Helpers;

namespace Test.Requests;

[TestClass]
public class VeiculoRequestTest
{
    [ClassInitialize]
    public static void ClassInit(TestContext context)
    {
        Setup.ClassInit(context);
    }

    [TestMethod]
    public async Task DeveCadastrarVeiculo_QuandoDadosForemValidos()
    {
        // Arrange
        var dto = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2020
        };

        var content = new StringContent(
            JsonSerializer.Serialize(dto),
            Encoding.UTF8,
            "application/json"
        );

        // Act
        var response = await Setup.client.PostAsync("/veiculos", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();
        Assert.IsFalse(string.IsNullOrEmpty(body));
    }

    [TestMethod]
    public async Task DeveRetornarListaDeVeiculos()
    {
        // Act
        var response = await Setup.client.GetAsync("/veiculos");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var veiculos = JsonSerializer.Deserialize<List<Veiculo>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.IsNotNull(veiculos);
    }

    [TestMethod]
    public async Task DeveRetornarVeiculoPorId_QuandoExistir()
    {
        // Act
        var response = await Setup.client.GetAsync("/veiculos/1");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var veiculo = JsonSerializer.Deserialize<Veiculo>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.IsNotNull(veiculo);
        Assert.IsTrue(veiculo!.Id > 0);
    }

    [TestMethod]
    public async Task DeveRetornarNotFound_QuandoVeiculoNaoExistir()
    {
        // Act
        var response = await Setup.client.GetAsync("/veiculos/999");

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [TestMethod]
    public async Task DeveAtualizarVeiculo_QuandoDadosForemValidos()
    {
        // Arrange
        var dto = new VeiculoDTO
        {
            Nome = "Corolla",
            Marca = "Toyota",
            Ano = 2022
        };

        var content = new StringContent(
            JsonSerializer.Serialize(dto),
            Encoding.UTF8,
            "application/json"
        );

        // Act
        var response = await Setup.client.PutAsync("/veiculos/1", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async Task DeveExcluirVeiculo_QuandoExistir()
    {
        // Act
        var response = await Setup.client.DeleteAsync("/veiculos/1");

        // Assert
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
    }
}
