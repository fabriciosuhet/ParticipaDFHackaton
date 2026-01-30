namespace SolucaoParticipaDF.API.Services.Interfaces
{
    public interface IDetectorIaService
    {
        /// <summary>
        /// Avalia semanticamente o texto utilizando um modelo de IA local.
        /// </summary>
        /// <param name="texto">Texto do pedido</param>
        /// <returns>
        /// Tupla contendo:
        /// - contemDados: indica se há dados pessoais
        /// - confianca: score de confiança do modelo
        /// </returns>
        (bool contemDados, double confianca) AvaliarTexto(string texto);
    }
}
