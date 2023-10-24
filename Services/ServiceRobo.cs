using RoboRegisAPI.Model;

namespace RoboRegisAPI.Services;
public class ServiceRobo
{
    private HttpClient _httpclient;
    private ServiceJson _serviceJson;   
    private ServicePlanilha _servicePlanilha;
    private ServiceAnvisaAPI _anvisaApi;
    private Apresentacao _apresentacao;
    
    public ServiceRobo()
    {
        _serviceJson = new ServiceJson();       
        _servicePlanilha = new ServicePlanilha();
        _anvisaApi = new ServiceAnvisaAPI();
        _httpclient  = new HttpClient();
        _apresentacao = new Apresentacao();
    }   
    private HttpClient GetHttpClient()
    {       
        //retorna o cliente Http
        return _httpclient;
    } 

    #region GERAR PLANILHA
    public async Task ConsultarProdutos()//Metodo gera um planilha com todas as Consultas
    {
        _apresentacao.MsgInicial();
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
                ConverterParaPlanilha(produtos);
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }   
    private List<string> RegistrosParaConsultar()
    {        
        //Retorna uma Lista com todos os registros para consultar na API
        return _servicePlanilha.TranformarList();
    }
    private void ConverterParaPlanilha(List<Produtos> produtos)
    {
        //Converte o List para Planilha
        _servicePlanilha.ConverterItemtoPlanilhaList(produtos);
    }   
    #endregion

    #region GERAR JSON E CSV
    public async Task ConsultarProdutosJson()//Metodo gera um arquivo CSV com todos os registros
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
                string contents = await _anvisaApi.ConsumirAPIJson(cliente, registros);
                
                //Gera um arquivo Json com todas as respostas
                _serviceJson.GerarArquivoJson(contents);

                //Le o arquivo e tranforma em um .CSV
                _serviceJson.LerArquivoJson();
                            
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private List<Produtos> DesserializarList(List<string> contents)
    {
        //Desserializar o List
        return _serviceJson.DesserializarJson(contents);
    }
    #endregion


    #region TESTE - NOVA PLANILHA COM FILTRO VENCIDO E CANCELADO
    private List<Registros> RegistrosParaConsultar3()
    {        
        //Retorna uma Lista com todos os registros para consultar na API
        return _servicePlanilha.TranformarList2();
    }

    public async Task ConsultarProdutos3()//Metodo gera um planilha com todas as Consultas
    {
        _apresentacao.MsgInicial();
        try
        {
            using (HttpClient client = new HttpClient())
            {
                //Lista de Registros para consultar
                List<Registros> registros = RegistrosParaConsultar3();

                //retorna o cliente Http
                var cliente = GetHttpClient();

                //Consumir Api da Anvisa
                List<string> contents = await _anvisaApi.ConsumirAPI3(cliente, registros);

                //Desseriaizar o List Contents para o Tipo Class Produtos
                List<Produtos> produtos = DesserializarList(contents);

                //Convert o List Produto para Planilha
                ConverterParaPlanilha(produtos);
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    #endregion
}