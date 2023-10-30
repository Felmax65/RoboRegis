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
            TransformarCSV(rootVigentes);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao Desserializar as consultas: {e.Message}");
        }
    }

    public void TransformarCSV(List<RootVigente> vigentes){

        using(var fluxo = new FileStream(@"C:\RoboRegis\Dados-RoboRegis\Saida\registros.csv", FileMode.Create))
        using(var escritor = new StreamWriter(fluxo)){

            foreach(var itens in vigentes){
                escritor.Write($"{itens.produto};{itens.empresa.cnpj};{itens.empresa.razaoSocial};{itens.registro};{itens.processo};{itens.cancelado};{itens.dataCancelamento};{itens.vencimento.data};{itens.vencimento.descricao};");
            }

        }

    }
}