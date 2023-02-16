using Domain.Entities;
using Domain.Ports;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AuthRepositorio : IdentityDbContext<UsuarioDaAplicacao>, IAuthRepositorio
{
    public AuthRepositorio(DbContextOptions<AuthRepositorio> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}