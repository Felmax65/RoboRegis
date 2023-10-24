using RoboRegisApi.Services;
namespace RoboRegis;
class RoboRegis
{
    static async Task Main (string[] args)
    {           
        ServiceRobo service = new ServiceRobo();
        //await service.ConsultaProdutosJson();
        //await service.ConsultaProdutosPlanilha();
        await service.ConsultarProdutosPlanilhaFiltrada();               
    }
}