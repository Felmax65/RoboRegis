using RoboRegisAPI.Services;

namespace RoboRegisAPI;
class RoboRegis
{
    static async Task Main (string[] args)
    {
        ConsumindoAPI c = new ConsumindoAPI();
        await c.ConsultaNmRegistro("80207450008");
    }   
}