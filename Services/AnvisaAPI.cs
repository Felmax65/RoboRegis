using RoboRegisAPI.testes;
namespace RoboRegisAPI.Services;

public class AnvisaAPI
{
    private DesserializarJson _desserializar;
    private ConvertToPlanilha _converter;
    public AnvisaAPI()
    {
        _desserializar = new DesserializarJson();
        _converter = new ConvertToPlanilha();
    }
   
    public async Task ConsultaNmRegistroList(List<teste> registros){

        using (HttpClient client = new HttpClient())
        {    
            //Adiciona o Guest ao Header da API      
            client.DefaultRequestHeaders.Add("Authorization" ,"Guest");          
            
            //Declaracao List Contetens
            List<string> contents = new List<string>();

            //Insercao de Json no List contents
            foreach(var itens in registros)
            {            
                //URL para chamar o metodo GET de consulta da API ANVISA 
                string url = $"https://consultas.anvisa.gov.br/api/consulta/genericos?count=10&filter%5BnumeroRegistro%5D={itens.nome}&page=1";

                //Resposta da API
                var response = await client.GetAsync(url);

                //Armazena o resultado da consulta em tipo String
                var content = await response.Content.ReadAsStringAsync();               
                
                //Adiciona os content dos registros para o List Contents
                contents.Add(content);               
            }
            
            //Desseriaizar o List Contents para o Tipo Class Produtos
            var produtos = _desserializar.ConverterParaJson(contents);
      
            //Convert o List Produto para Planilha
            _converter.ConverterItemtoPlanilhaList(produtos);
        }	
    }
}