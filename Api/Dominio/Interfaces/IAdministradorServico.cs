    using MinimalApi.Dominio.Entidades;
    using MinimalApi.DTOs;



    namespace MinimalApi.Dominio.interfaces;

    public interface IAdministradorServico
    {
        Task<Administrador?> Login(LoginDTO loginDTO);
        Task Incluir(Administrador administrador);
        Administrador? BuscaPorId(int id);
        List<Administrador> Todos(int? pagina);


    }


