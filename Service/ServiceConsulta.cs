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

    #region Consultar
    //Metodo responsavel por consultar os registros consumindo a API da ANVISA
    public async Task<List<string>> Consulta(){
        try{
            var registros = _servicePlanilha.ConverterRegistros(); // Converte os registros para o tipo List Registros

            foreach(var itens in registros){ // leitura do List Registros
                Url = $"https://consultas.anvisa.gov.br/api/consulta/saude/{itens.NmProcesso}/?count=10&filter%5BnumeroRegistro%5D={itens.NmRegistro}&page=1. "; // UrL para consultas na api da ANVISA
                var content = await _serviceApi.ConsumirApi(Url); // Consume a API a partir da URL informada e retorna a resposta
                if(content != null){ // Verifica se content e null
                    Contents.Add(content); // Armazena a resposta em um List do tipo string
                }
                System.Console.WriteLine($"{Contents.Count()}");                        
            }
            return Contents; 
        }
        catch(Exception e){
            Console.ForegroundColor = ConsoleColor.Red;
            throw new Exception($"Ocorreu algo de errado com a consulta :{e.Message}");
        }
    }
    #endregion
}