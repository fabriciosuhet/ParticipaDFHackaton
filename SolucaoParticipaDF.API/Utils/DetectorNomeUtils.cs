using System.Text.RegularExpressions;

namespace SolucaoParticipaDF.API.Services
{
    public static class DetectorNomeUtils
    {
        // Palavras que parecem nomes (Maiúsculas) mas NÃO SÃO.
        // Adicione aqui termos comuns em ofícios do GDF.
        private static readonly HashSet<string> PalavrasIgnoradas = new(StringComparer.OrdinalIgnoreCase)
        {
            "A", "O", "As", "Os", "De", "Da", "Do", "Dos", "Das", "E", "Em", "Para", "Com",
            "Solicito", "Solicitamos", "Gostaria", "Prezados", "Senhor", "Senhora", "Excelentíssimo",
            "Venho", "Atenciosamente", "Cordialmente", "Secretaria", "Diretoria", "Gerencia",
            "Departamento", "Governo", "Distrito", "Federal", "Brasília", "Processo", "Sei",
            "Pedido", "Requerimento", "Informação", "Acesso", "Lei", "Data", "Anexo", "Cópia",
            "Bom", "Dia", "Tarde", "Noite", "Grato", "Aguardo", "Deferimento", "Mesmo", "Mesma"
        };

        // Gatilhos que indicam que a próxima palavra PROVAVELMENTE é um nome
        private static readonly string[] GatilhosDeContexto =
        {
            "eu, ", "eu ", "chamo ", "nome ", "servidor ", "servidora ", "paciente ",
            "aluno ", "aluna ", "cidadão ", "cidadã ", "sr. ", "sra. ", "dr. ", "dra. "
        };

        public static bool PossuiNomeDePessoa(string texto, out double confianca)
        {
            confianca = 0.0;
            if (string.IsNullOrWhiteSpace(texto)) return false;

            var textoLimpo = texto.Replace("\r", " ").Replace("\n", " ");

            // 2. Busca por Padrões de Capitalização (Ex: "João da Silva")
            // Regex explica: Palavra Maiúscula + (opcional: conector minúsculo + Palavra Maiúscula) repetido
            // Ex: Pega "Maria Souza", "Pedro de Alcantara", "Ana da Silva"
            var regexNomeComposto = new Regex(
                @"\b[A-ZÀ-Ú][a-zà-ú]+(?:\s+(?:d[aeo]s?|e)\s+[A-ZÀ-Ú][a-zà-ú]+|\s+[A-ZÀ-Ú][a-zà-ú]+)+\b",
                RegexOptions.Compiled);

            var matches = regexNomeComposto.Matches(textoLimpo);

            foreach (Match match in matches)
            {
                var candidato = match.Value;
                var partes = candidato.Split(' ');

                // Regra A: Se qualquer parte do nome estiver na Blacklist, descarta.
                // Ex: "Secretaria de Saúde" (Secretaria e Saúde são ignorados)
                if (partes.Any(p => PalavrasIgnoradas.Contains(p)))
                    continue;

                // Regra B: Contexto (Aumenta muito a confiança)
                // Verifica se antes do nome tem um gatilho ("servidor João...")
                int indexGatilho = Math.Max(0, match.Index - 20); // Olha 20 chars pra trás
                string contextoAnterior = textoLimpo.Substring(indexGatilho, match.Index - indexGatilho).ToLower();

                bool temGatilho = GatilhosDeContexto.Any(g => contextoAnterior.Contains(g));

                if (temGatilho)
                {
                    confianca = 0.95;
                    return true;
                }

                // Regra C: Se não tem gatilho, mas passou no Regex e na Blacklist
                // Assumimos que é nome com confiança média/alta
                confianca = 0.85;
                return true;
            }

            return false;
        }
    }
}