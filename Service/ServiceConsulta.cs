namespace RRegis.Service;
public class ServiceConsulta{
    private ServiceApi _serviceApi;
    private ServicePlanilha _servicePlanilha;
    private List<string> Contents;
    public string Url { get; set; }
    public ServiceConsulta(){
        Contents = new List<string>();
        _serviceApi = new ServiceApi();
        _servicePlanilha = new ServicePlanilha();
    }
    public async Task<List<string>> Consulta(){
        var registros = _servicePlanilha.ConverterRegistros();

        foreach(var itens in registros){
            Url = $"https://consultas.anvisa.gov.br/api/consulta/saude/{itens.NmProcesso}/?count=10&filter%5BnumeroRegistro%5D={itens.NmRegistro}&page=1. ";
            var content = await _serviceApi.ConsumirApi(Url);
            Contents.Add(content);
            //await Task.Delay(5000);
            System.Console.WriteLine($"{Contents.Count()}");            
        }
        return Contents;       
    }
}