using SolucaoParticipaDF.API.Models;

namespace SolucaoParticipaDF.API.Services.Interfaces
{
    public interface IOrquestradorDeteccaoService
    {
        /// <summary>
        /// Executa o fluxo completo de detecção de dados pessoais em um texto.
        /// </summary>
        /// <param name="texto">Texto do pedido</param>
        /// <returns>Resultado da detecção</returns>
        ResultadoDeteccao Processar(string texto);
    }
}
