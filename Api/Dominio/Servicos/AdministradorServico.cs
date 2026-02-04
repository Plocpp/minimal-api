using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;
using MinimalApi.Dominio.interfaces;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Dominio.Servicos;

public class AdministradorServico : IAdministradorServico
{
    private readonly DbContexto _contexto;

    public AdministradorServico(DbContexto contexto)
    {
        _contexto = contexto;
    }

    public Administrador? BuscaPorId(int id)
    {
        return _contexto.Administradores.FirstOrDefault(a => a.Id == id);
    }

    public void Incluir(Administrador administrador)
    {
        _contexto.Administradores.Add(administrador);
        _contexto.SaveChanges();
    }


    public async Task<Administrador?> Login(LoginDTO loginDTO)
    {
        return await _contexto.Administradores
            .FirstOrDefaultAsync(a =>
                a.Email == loginDTO.Email &&
                a.Senha == loginDTO.Senha);
    }

    public List<Administrador> Todos(int? pagina)
    {
        var query = _contexto.Administradores.AsQueryable();int itensPorPagina = 10;

        if (pagina != null)
        {
            query = query
                .Skip(((int)pagina - 1) * itensPorPagina)
                .Take(itensPorPagina);
        }

        return query.ToList();
    }

    Task IAdministradorServico.Incluir(Administrador administrador)
    {
        throw new NotImplementedException();
    }
}
