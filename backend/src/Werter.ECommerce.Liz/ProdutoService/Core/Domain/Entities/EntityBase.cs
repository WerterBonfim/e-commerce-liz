namespace Domain.Entities;

public abstract class EntityBase
{
    public Guid Id { get; }
    public DateTime DataHoraCadastro { get; }
    public DateTime DataHoraAlteracao { get; private set; }
    public bool Ativo { get; private set; }

    public EntityBase()
    {
        Id = Guid.NewGuid();
        DataHoraCadastro = DateTime.Now;
    }

    public void HouveAtualizacao() => DataHoraAlteracao = DateTime.Now;

    public void Desativar()
    {
        Ativo = false;
        HouveAtualizacao();
    }

    public void Ativar()
    {
        Ativo = true;
        HouveAtualizacao();
    }

}