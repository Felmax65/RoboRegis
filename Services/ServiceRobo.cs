using RoboRegisAPI.Model;

namespace RoboRegisAPI.Services;
public class ServiceRobo
{
    private HttpClient _httpclient;
    private DesserializarJson _desserializar;
    private ConvertToPlanilha _converter;
    private ServicePlanilha _servicePlanilha;
    private AnvisaAPI _anvisaApi;
    private ServiceCSV _serviceCsv;
    public ServiceRobo()
    {
        _desserializar = new DesserializarJson();
        _converter = new ConvertToPlanilha();
        _servicePlanilha = new ServicePlanilha();
        _anvisaApi = new AnvisaAPI();
        _httpclient  = new HttpClient();
        _serviceCsv = new ServiceCSV();
    }
   
    private HttpClient GetHttpClient()
    {       
        //retorna o cliente Http
        return _httpclient;
    } 
    public async Task ConsultaNmRegistro()
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                //Lista de Registros para consultar
                List<string> registros = RegistrosParaConsultar();

                //retorna o cliente Http
                var cliente = GetHttpClient();

                //Consumir Api da Anvisa
                List<string> contents = await _anvisaApi.ConsumirAPI(cliente, registros);

                //Desseriaizar o List Contents para o Tipo Class Produtos
                List<Produtos> produtos = DesserializarList(contents);

                //Convert o List Produto para Planilha
                //ConverterParaPlanilha(produtos);

                //Convertet para CSV
                ConverterToCsv(produtos);
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }

    private void ConverterToCsv(List<Produtos> produtos)
    {
        _serviceCsv.TrasformarCSV(produtos);
    }

    private List<string> RegistrosParaConsultar()
    {        
        //Retorna uma Lista com todos os registros para consultar na API
        return _servicePlanilha.TranformarList();
    }


    private void ConverterParaPlanilha(List<Produtos> produtos)
    {
        //Converte o List para Planilha
        _converter.ConverterItemtoPlanilhaList(produtos);
    }


    private List<Produtos> DesserializarList(List<string> contents)
    {
        //Desserializar o List
        return _desserializar.ConverterParaJson(contents);
    }

}