using System.Net;
using RoboRegisAPI.Model;

namespace RoboRegisAPI.Services;
public class ServiceAnvisaAPI
{   
    private ServiceJson _serviceJson;
    public ServiceAnvisaAPI()
    {
        _serviceJson = new ServiceJson();
    }
    public async Task<List<string>> ConsumirAPI(HttpClient client, List<string> registros)
    {
        //Declaracao List Contetens
        List<string> contents = new List<string>();

        try
        {
            //Adiciona o Guest ao Header da API  
            client.DefaultRequestHeaders.Add("Authorization", "Guest");

            //Insercao de Json no List contents
            foreach (var itens in registros)
            {
                //URL para chamar o metodo GET de consulta da API ANVISA
                string url = $"https://consultas.anvisa.gov.br/api/consulta/genericos?count=10&filter%5BnumeroRegistro%5D={itens}&page=1";

                //Resposta da API
                var response = await client.GetAsync(url);

                //Armazena o resultado da consulta em tipo String
                var content = await response.Content.ReadAsStringAsync();

                //Adiciona os content dos registros para o List Contents
                contents.Add(content);
            }
        }
        catch(Exception e)
        {
            System.Console.WriteLine(e.Message);
        }

        return contents;
    }
    public async Task<string> ConsumirAPI2(HttpClient client, List<string> registros)
    {
        //Declaracao List Contetens
        List<ProdutosContent> contents = new List<ProdutosContent>();
        string combinedJson="";

        try
        {
            //Realiza a consulta dos registros
            await ConsultarAPI(client, registros, contents);

            //Combina as respostas em uma unica string
            combinedJson = _serviceJson.CombinarJson(contents);

        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }

        return combinedJson;
    }
    private async Task ConsultarAPI(HttpClient client, List<string> registros, List<ProdutosContent> contents)
    {
        client.DefaultRequestHeaders.Add("Authorization", "Guest");

        //Insercao de Json no List contents
        foreach (var itens in registros)
        {
            //URL para chamar o metodo GET de consulta da API ANVISA
            string url = $"https://consultas.anvisa.gov.br/api/consulta/genericos?count=10&filter%5BnumeroRegistro%5D={itens}&page=1";

            //Resposta da API
            var response = await client.GetAsync(url);
            var respostas = "";

            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Verificacao de respostas com status 200, caso seja outro status a resposta nao é inserida na string
                respostas = await response.Content.ReadAsStringAsync();
            }

            //Desserializa as respostas
            _serviceJson.DesserializarRespostas(contents, respostas);
        }
    }
    
}