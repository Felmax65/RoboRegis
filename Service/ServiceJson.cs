using Newtonsoft.Json.Linq;
using RRegis.Model.ModelJson.Cancelado;
using RRegis.Model.ModelJson.Vencidos;
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
        List<RootVencido> rootVencidos = new List<RootVencido>();
        List<RootCancelado> rootCancelados = new List<RootCancelado>();

        try
        {
            foreach (var jsonString in consultas)
            {
                var jObject = JsonConvert.DeserializeObject<JObject>(jsonString);

                if (jObject["vencimento"] != null)
                {
                    var vencimento = jObject["vencimento"];
                    
                    if (vencimento["descricao"] != null && vencimento["descricao"].Value<string>() == "VIGENTE")
                    {
                        var vigentes = JsonConvert.DeserializeObject<RootVigente>(jsonString);
                        rootVigentes.Add(vigentes);
                    }
                    else if (vencimento["data"] != null)
                    {
                        var vencidos = JsonConvert.DeserializeObject<RootVencido>(jsonString);
                        rootVencidos.Add(vencidos);
                    }
                }
                else if (jObject["cancelado"] != null)
                {
                    var cancelados = JsonConvert.DeserializeObject<RootCancelado>(jsonString);
                    rootCancelados.Add(cancelados);
                }
            }
            _servicePlanilha.GerarPlanilha(rootVigentes, rootVencidos, rootCancelados);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao Desserializar as consultas: {e.Message}");
        }
    }
}