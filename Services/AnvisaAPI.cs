namespace RoboRegisAPI.Services;

public class AnvisaAPI
{   
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
}