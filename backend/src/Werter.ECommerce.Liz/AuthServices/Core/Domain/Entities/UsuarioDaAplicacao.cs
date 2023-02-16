using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public sealed class UsuarioDaAplicacao : IdentityUser
{
    public string Nome { get; set; }
    public string SobreNome { get; set; }
}