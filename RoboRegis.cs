using RoboRegisAPI.Services;
namespace RoboRegisAPI;
class RoboRegis
{
    static async Task Main (string[] args)
    {           
        ServiceRobo service = new ServiceRobo();
        await service.ConsultaNmRegistro3();
        await service.ConsultaNmRegistro();               
    }
}