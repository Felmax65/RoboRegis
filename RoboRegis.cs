using RoboRegisAPI.Service;

namespace RoboRegisAPI;
class RoboRegis
{
    static async Task Main (string[] args)
    {
        ConsumindoAPI c = new ConsumindoAPI();
        await c.ConsultaNmRegistro2("80207450008");
    }   
}