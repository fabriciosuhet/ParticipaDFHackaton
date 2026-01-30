using OfficeOpenXml;
using SolucaoParticipaDF.API.Models;
using SolucaoParticipaDF.API.Services.Interfaces;

namespace SolucaoParticipaDF.API.Services
{
    public class LeitorXlsxService : ILeitorXlsxService
    {
        public List<RequisicaoDeteccao> LerPedidos(Stream arquivoXlsx)
        {
            ExcelPackage.License.SetNonCommercialPersonal("Solução Participa DF");

            var requisicoes = new List<RequisicaoDeteccao>();

            using var package = new ExcelPackage(arquivoXlsx);
            var worksheet = package.Workbook.Worksheets.First();

            var totalLinhas = worksheet.Dimension.Rows;

            // Considerando:
            // Coluna A = ID
            // Coluna B = Texto do Pedido
            for (int linha = 2; linha <= totalLinhas; linha++)
            {
                var textoPedido = worksheet.Cells[linha, 2].Text;

                if (string.IsNullOrWhiteSpace(textoPedido))
                    continue;

                requisicoes.Add(new RequisicaoDeteccao
                {
                    Texto = textoPedido
                });
            }

            return requisicoes;
        }
    }
}
