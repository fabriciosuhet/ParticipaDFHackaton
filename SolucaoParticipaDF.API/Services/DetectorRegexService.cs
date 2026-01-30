using SolucaoParticipaDF.API.Patterns;
using SolucaoParticipaDF.API.Services.Interfaces;

namespace SolucaoParticipaDF.API.Services
{
    public class DetectorRegexService : IDetectorRegexService
    {
        public List<string> IdentificarTipos(string texto)
        {
            var tiposEncontrados = new List<string>();

            if (string.IsNullOrWhiteSpace(texto))
                return tiposEncontrados;

            // Verifica CPF
            if (PadroesDadosPessoais.Cpf.IsMatch(texto))
            {
                tiposEncontrados.Add("CPF");
            }

            // Verifica RG
            if (PadroesDadosPessoais.Rg.IsMatch(texto))
            {
                tiposEncontrados.Add("RG");
            }

            // Verifica Email
            if (PadroesDadosPessoais.Email.IsMatch(texto))
            {
                tiposEncontrados.Add("Email");
            }

            // Verifica Telefone
            if (PadroesDadosPessoais.Telefone.IsMatch(texto))
            {
                tiposEncontrados.Add("Telefone");
            }

            return tiposEncontrados;
        }
    }
}