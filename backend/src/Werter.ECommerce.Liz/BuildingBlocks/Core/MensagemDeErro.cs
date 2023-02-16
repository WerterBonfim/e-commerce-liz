namespace Core;

public class MensagemDeErro
{
    public static string ExcedeuMaximoDeCaracteres(string campo, int qtd) 
        => $"O campo {campo} não pode ter mais de {qtd} caracteares";
    
    public static string MenorQueMinimoDeCaracteres(string campo, int qtd) 
        => $"O campo {campo} está inválido, tem menos de {qtd} caracteares";
    
    public static string CampoInvalido(string? campo) => $"Campo {campo} está inválido."; 

}