namespace Test.Domain.Entidades;

[TestClass]
public sealed class VeiculosTest
{
    [TestMethod]
    public void TestarGetSetPropriedadesVeiculos()
    {
        //Arrange (são as variaveis que são criadas para os testes)
        var veiculo = new Veiculo();

        //act (ação que serão executadas durante o processo)
        veiculo.Id = 1;
        veiculo.Nome = "carro1";
        veiculo.Marca = "marca1";
        veiculo.Ano = 2025;

        // assert (é a validação dos dados utilizados)

        Assert.AreEqual(1, veiculo.Id);
        Assert.AreEqual("carro1", veiculo.Nome);
        Assert.AreEqual("marca1", veiculo.Marca);
        Assert.AreEqual(2025, veiculo.Ano);
    }
}
