using RoboRegisAPI.Service;

namespace RoboRegisAPI;
class RoboRegis
{
    static async Task Main (string[] args)
    {
        await ConsumindoAPI.ConsultaNmRegistro("80207450008");
    }   
}