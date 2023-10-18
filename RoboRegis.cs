using RoboRegisAPI.Services;
using RoboRegisAPI.testes;
namespace RoboRegisAPI;
class RoboRegis
{
    static async Task Main (string[] args)
    {
        //ConsumindoAPI c = new ConsumindoAPI();
        //await c.ConsultaNmRegistro("81944280002");

        List<teste> nm = new List<teste>();
        nm.Add(new teste("80677040003"));
        nm.Add(new teste("80117580501"));
        nm.Add(new teste("80488299007"));
        nm.Add(new teste("80488290011"));
        nm.Add(new teste("80488290032"));
        nm.Add(new teste("80124940001"));
        nm.Add(new teste("80124940003"));
        nm.Add(new teste("80488290001"));
        nm.Add(new teste("80117610012"));
        nm.Add(new teste("80412230001"));
      
        AnvisaAPI c = new AnvisaAPI();
        await c.ConsultaNmRegistroList(nm);
        
    }
}