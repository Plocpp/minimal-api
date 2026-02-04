using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.interfaces;

namespace Test.Mocks
{
    public class VeiculoServicoMock : IVeiculoServico
    {
        private static List<Veiculo> veiculos = new();

        public VeiculoServicoMock()
        {
            // garante estado conhecido em cada teste
            if (!veiculos.Any())
            {
                veiculos.Add(new Veiculo
                {
                    Id = 1,
                    Nome = "Gol",
                    Marca = "VW",
                    Ano = 2020
                });
            }
        }

        public List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null)
        {
            return veiculos;
        }

        public Veiculo? BuscaPorId(int id)
        {
            return veiculos.FirstOrDefault(v => v.Id == id);
        }

        public void Incluir(Veiculo veiculo)
        {
            veiculo.Id = veiculos.Any() ? veiculos.Max(v => v.Id) + 1 : 1;
            veiculos.Add(veiculo);
        }

        public void Atualizar(Veiculo veiculo)
        {
            // como é lista por referência, não precisa fazer nada
        }

        public void Apagar(Veiculo veiculo)
        {
            veiculos.Remove(veiculo);
        }
    }
}
