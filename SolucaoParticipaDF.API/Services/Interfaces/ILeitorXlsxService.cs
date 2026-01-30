using SolucaoParticipaDF.API.Models;

namespace SolucaoParticipaDF.API.Services.Interfaces
{
    public interface ILeitorXlsxService
    {
        List<RequisicaoDeteccao> LerPedidos(Stream arquivoXlsx);
    }
}
