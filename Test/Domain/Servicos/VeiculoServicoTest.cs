using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Infraestrutura.Db;
using Xunit;
using Assert = Xunit.Assert;

namespace MinimalApi.Test.Dominio.Servicos
{
    public class VeiculoServicoTest
    {
        private DbContexto CriarContexto()
        {
            var options = new DbContextOptionsBuilder<DbContexto>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new DbContexto(options);
 // ✅ AQUI
        }

        [Fact]
        public void Deve_Incluir_Veiculo()
        {
            var contexto = CriarContexto();
            var servico = new VeiculoServico(contexto);

            servico.Incluir(new Veiculo
            {
                Nome = "Gol",
                Marca = "Volkswagen",
                Ano = 2020
            });

            Assert.Equal(1, contexto.Veiculos.Count());
        }

        [Fact]
        public void Deve_Buscar_Veiculo_Por_Id()
        {
            var contexto = CriarContexto();
            var servico = new VeiculoServico(contexto);

            servico.Incluir(new Veiculo
            {
                Nome = "Civic",
                Marca = "Honda",
                Ano = 2021
            });

            var resultado = servico.BuscaPorId(1);

            Assert.NotNull(resultado);
            Assert.Equal("Civic", resultado!.Nome);
        }

        [Fact]
        public void Deve_Atualizar_Veiculo()
        {
            var contexto = CriarContexto();
            var servico = new VeiculoServico(contexto);

            var veiculo = new Veiculo
            {
                Nome = "Uno",
                Marca = "Fiat",
                Ano = 2019
            };

            servico.Incluir(veiculo);
            veiculo.Nome = "Uno Mille";
            servico.Atualizar(veiculo);

            var atualizado = servico.BuscaPorId(veiculo.Id);

            Assert.Equal("Uno Mille", atualizado!.Nome);
        }

        [Fact]
        public void Deve_Apagar_Veiculo()
        {
            var contexto = CriarContexto();
            var servico = new VeiculoServico(contexto);

            var veiculo = new Veiculo
            {
                Nome = "Palio",
                Marca = "Fiat",
                Ano = 2018
            };

            servico.Incluir(veiculo);
            servico.Apagar(veiculo);

            Assert.Empty(contexto.Veiculos);
        }

        [Fact]
        public void Deve_Listar_Todos_Veiculos()
        {
            var contexto = CriarContexto();
            var servico = new VeiculoServico(contexto);

            servico.Incluir(new Veiculo { Nome = "Onix", Marca = "Chevrolet", Ano = 2022 });
            servico.Incluir(new Veiculo { Nome = "HB20", Marca = "Hyundai", Ano = 2023 });

            var lista = servico.Todos();

            Assert.Equal(2, lista.Count);
        }
    }
}
