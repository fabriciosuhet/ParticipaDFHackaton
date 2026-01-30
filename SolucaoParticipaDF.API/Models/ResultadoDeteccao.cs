namespace SolucaoParticipaDF.API.Models
{
    public class ResultadoDeteccao
    {
        /// <summary>
        /// Indica se o texto contém dados pessoais.
        /// </summary>
        public bool ContemDadosPessoais { get; set; }

        /// <summary>
        /// Indica os dados identificados.
        /// </summary>
        public List<string> TiposDadosIdentificados { get; set; } = []; // Inicializa uma lista vázia.

        /// <summary>
        /// Motivo da classificação (Regex ou IA).
        /// </summary>
        public string Motivo { get; set; } = string.Empty;

        /// <summary>
        /// Grau de confiança da decisão (0 a 1).
        /// </summary>
        public double Confianca { get; set; }
    }
}
