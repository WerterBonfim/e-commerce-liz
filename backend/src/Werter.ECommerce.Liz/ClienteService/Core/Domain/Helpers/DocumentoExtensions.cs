namespace Domain.Helpers;

public static class DocumentoExtensions
{
    public static string LimparDocumento(this string documento) => documento
        .Replace(".", "")
        .Replace("-", "");
}