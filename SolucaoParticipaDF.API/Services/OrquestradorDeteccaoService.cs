using SolucaoParticipaDF.API.Models;
using SolucaoParticipaDF.API.Services.Interfaces;

namespace SolucaoParticipaDF.API.Services
{
    public class OrquestradorDeteccaoService : IOrquestradorDeteccaoService
    {
        private readonly IDetectorRegexService _regex;
        private readonly IDetectorIaService _ia;

        public OrquestradorDeteccaoService(IDetectorRegexService regex, IDetectorIaService ia)
        {
            _regex = regex;
            _ia = ia;
        }

        public ResultadoDeteccao Processar(string texto)
        {

            var tiposEncontrados = _regex.IdentificarTipos(texto);

            var (iaDetectou, confiancaIa) = _ia.AvaliarTexto(texto);

            if (iaDetectou)
            {
                tiposEncontrados.Add("Nome");
            }

            if (tiposEncontrados.Count > 0)
            {
                // Se temos Regex (1.0) e IA (0.85), usamos a maior confiança ou média ponderada.
                // Aqui vou priorizar a certeza absoluta do Regex (1.0) se houver, 
                // senão uso a da IA.
                double confiancaFinal = tiposEncontrados.Any(t => t != "Nome") ? 1.0 : confiancaIa;

                string motivoFinal = "Dados pessoais identificados.";
                if (tiposEncontrados.Contains("Nome") && tiposEncontrados.Count == 1)
                    motivoFinal = "Nome identificado por análise de padrão (IA Simbólica).";
                else if (tiposEncontrados.Count > 0)
                    motivoFinal = "Múltiplos dados identificados (Regex + Padrão).";

                return new ResultadoDeteccao
                {
                    ContemDadosPessoais = true,
                    TiposDadosIdentificados = tiposEncontrados.Distinct().ToList(),
                    Motivo = motivoFinal,
                    Confianca = confiancaFinal
                };
            }

            return new ResultadoDeteccao
            {
                ContemDadosPessoais = false,
                TiposDadosIdentificados = [],
                Motivo = "Nenhum dado pessoal identificado.",
                Confianca = 0.0
            };
        }
    }
}