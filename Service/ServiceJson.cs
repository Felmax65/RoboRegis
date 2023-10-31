using RRegis.Model.ModelJson.Vigente;
using Newtonsoft.Json;
namespace RRegis.Service;
public class ServiceJson{    
    private ServicePlanilha _servicePlanilha;
    public ServiceJson(){
        _servicePlanilha = new ServicePlanilha();
    }
    public void Desserializar(List<string> consultas){        
        List<RootVigente> rootVigentes = new List<RootVigente>();
        //List<RootVencido> rootVencidos = new List<RootVencido>();
        //List<RootCancelado> rootCancelados = new List<RootCancelado>();

        try
        {
            foreach (var jsonString in consultas)
            {
                var vigentes = JsonConvert.DeserializeObject<RootVigente>(jsonString);
                rootVigentes.Add(vigentes);                       
            }
            _servicePlanilha.GerarPlanilha(rootVigentes);           
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro ao Desserializar as consultas: {e.Message}");
        }
    }
}