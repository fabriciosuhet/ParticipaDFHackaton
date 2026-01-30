using System.Text.RegularExpressions;

namespace SolucaoParticipaDF.API.Patterns
{
    /// <summary>
    /// Centraliza os padrões Regex utilizados para identificação
    /// de dados pessoais explícitos.
    /// </summary>
    public static class PadroesDadosPessoais
    {
        public static readonly Regex Cpf =
            new(@"\b\d{3}\.\d{3}\.\d{3}-\d{2}\b|\b\d{11}\b");

        public static readonly Regex Email =
            new(@"\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b");

        public static readonly Regex Telefone =
            new(@"\(?\d{2}\)?\s?\d{4,5}-?\d{4}");

        public static readonly Regex Rg =
            new(@"\b\d{1,2}\.\d{3}\.\d{3}-?[0-9Xx]?\b");

    }
}
