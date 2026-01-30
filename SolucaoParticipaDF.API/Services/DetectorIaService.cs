using SolucaoParticipaDF.API.Services.Interfaces;

namespace SolucaoParticipaDF.API.Services
{
    /// <summary>
    /// Implementação inicial do detector de IA.
    /// Esta versão é um stub e será substituída futuramente
    /// por um modelo ONNX de classificação de texto.
    /// </summary>
    public class DetectorIaService : IDetectorIaService
    {
        public (bool contemDados, double confianca) AvaliarTexto(string texto)
        {
            if (DetectorNomeUtils.PossuiNomeDePessoa(texto, out double confianca))
            {
                return (true, confianca);
            }

            return (false, 0.0);
        }
    }
}
