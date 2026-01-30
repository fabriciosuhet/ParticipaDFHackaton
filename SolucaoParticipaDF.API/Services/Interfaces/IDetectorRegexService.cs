namespace SolucaoParticipaDF.API.Services.Interfaces
{
    public interface IDetectorRegexService
    {
        /// <summary>
        /// Verifica o texto e retorna uma lista com os tipos de dados encontrados (ex: CPF, Email).
        /// Retorna uma lista vazia se nada for encontrado.
        /// </summary>
        List<string> IdentificarTipos(string texto);
    }
}
