using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.interfaces;
using MinimalApi.DTOs;

namespace Test.Mocks
{
    public class AdministradorServicoMock : IAdministradorServico
    {
        private static List<Administrador> administradores = new()
        {
            new Administrador
            {
                Id = 1,
                Email = "teste@teste.com",
                Senha = "teste",
                Perfil = "Adm"
            },
            new Administrador
            {
                Id = 2,
                Email = "editor@teste.com",
                Senha = "123456",
                Perfil = "Editor"
            }
        };

        public Administrador? BuscaPorId(int id)
        {
            return administradores.FirstOrDefault(a => a.Id == id);
        }

        public List<Administrador> Todos(int? pagina)
        {
            return administradores;
        }

        public Task<Administrador?> Login(LoginDTO loginDTO)
        {
            var adm = administradores.FirstOrDefault(a =>
                a.Email == loginDTO.Email &&
                a.Senha == loginDTO.Senha);

            return Task.FromResult(adm);
        }

        public Task Incluir(Administrador administrador)
        {
            administrador.Id = administradores.Count + 1;
            administradores.Add(administrador);

            return Task.CompletedTask;
        }
    }
}