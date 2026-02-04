using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Infraestrutura.Db;
using System;

namespace Test.Domain.Servicos
{
    [TestClass]
    public class AdministradorServicoTest
    {
        private DbContexto CriarContexto()
        {
            var options = new DbContextOptionsBuilder<DbContexto>()
                .UseInMemoryDatabase(databaseName: $"Teste_{Guid.NewGuid()}")
                .Options;

            return new DbContexto(options);
        }

        [TestMethod]
        public void Deve_Buscar_Administrador_Por_Id()
        {
            // Arrange
            var contexto = CriarContexto();
            var servico = new AdministradorServico(contexto);

            var adm = new Administrador
            {
                Email = "teste@teste.com",
                Senha = "123",
                Perfil = "Adm"
            };

            servico.Incluir(adm);

            // Act
            var resultado = servico.BuscaPorId(adm.Id);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(adm.Id, resultado!.Id);
        }
    }
}
    