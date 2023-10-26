using System.Net;
using RoboRegisApi.Model;

namespace RoboRegisApi.Services;
public class ServiceAnvisaAPI
{   
    private HttpResponseMessage _response;
    private ServiceJson _serviceJson;
    private HttpClient _client;
    public ServiceAnvisaAPI()
    {
        _response = new HttpResponseMessage();
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Add("Authorization", "Guest");
        _serviceJson = new ServiceJson();
    }
  
    public async Task<List<string>> ConsumirAPI3(HttpClient client, List<Registros> registros)
    {
        //Declaracao List Contetens
        List<string> contents = new List<string>();
        string content;
        try
        {
            //Adiciona o Guest ao Header da API  
            client.DefaultRequestHeaders.Add("Authorization", "Guest");

            //Insercao de Json no List contents
            foreach (var itens in registros)
            {
                //URL para chamar o metodo GET de consulta da API ANVISA
                string url = $"https://consultas.anvisa.gov.br/api/consulta/genericos?count=10&filter%5BnumeroRegistro%5D={itens.Registro}&page=1";

                //Verificar status code da Resposta da API
                _response = await client.GetAsync(url);
                if(_response.StatusCode == HttpStatusCode.InternalServerError){
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("-API Offiline");
                    break;
                }
                if (_response.StatusCode == HttpStatusCode.OK){                    
                    content = await _response.Content.ReadAsStringAsync();
                    contents.Add(content);
                }
                else{
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($"-Registro do item: {itens.Registro} \nCodigo do status = {_response.StatusCode}");
                    contents = null;                    
                } 
            }
                
        }
        catch(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine($"-Erro ao se conectar com a API :",e.Message);
            
        }

        return contents;
    } 

    #region TESTES NOVO PRECISO API URGENTE
    public async Task<List<string>> ConsumirNOVO(HttpClient client, List<Registrosteste> registros)
    {
        //Declaracao List Contetens
        List<string> contents = new List<string>();
        string content;
        try
        {
            //Adiciona o Guest ao Header da API  
            client.DefaultRequestHeaders.Add("Authorization", "Guest");

            //Insercao de Json no List contents
            foreach (var itens in registros)
            {
                await Task.Delay(60000);
                //URL para chamar o metodo GET de consulta da API ANVISA
                string url = $"https://consultas.anvisa.gov.br/api/consulta/saude/{itens.Processo}/?count=10&filter%5BnumeroRegistro%5D={itens.Registro}&page=1";

                //Verificar status code da Resposta da API
                _response = await client.GetAsync(url);
                if(_response.StatusCode == HttpStatusCode.InternalServerError){
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("-API Offiline");
                    break;
                }
                if (_response.StatusCode == HttpStatusCode.OK){                    
                    content = await _response.Content.ReadAsStringAsync();
                    contents.Add(content);
                }
                else{
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($"-Registro do item: {itens.Registro} \nCodigo do status = {_response.StatusCode}");
                    contents = null;                    
                } 
            }
                
        }
        catch(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine($"-Erro ao se conectar com a API :",e.Message);
            
        }

        return contents;
    } 
    #endregion

}