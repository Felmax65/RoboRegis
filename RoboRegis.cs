using RoboRegisAPI.Services;
namespace RoboRegisAPI;
class RoboRegis
{
    static async Task Main (string[] args)
    {           
        ServiceRobo service = new ServiceRobo();
        //await service.ConsultaProdutos2();
        await service.ConsultarProdutos();               
    }
}