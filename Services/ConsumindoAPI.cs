using OpenQA.Selenium.DevTools.V116.Runtime;
using RoboRegisAPI.Model;
using RoboRegisAPI.testes;
namespace RoboRegisAPI.Services;

public class ConsumindoAPI
{
    public DesserializarJson _desser;
    public async Task ConsultaNmRegistro(string Nregistro){

        using (HttpClient client = new HttpClient())
        {    
            //Adiciona o Guest ao Header da API      
            client.DefaultRequestHeaders.Add("Authorization" ,"Guest");            
            
            //URL para chamar o metodo GET de consulta da API ANVISA 
            string url = $"https://consultas.anvisa.gov.br/api/consulta/genericos?count=10&filter%5BnumeroRegistro%5D={Nregistro}&page=1";

            //Resposta da API
            var response = await client.GetAsync(url);

            //Converte o response Json em um content string
            var content = await response.Content.ReadAsStringAsync();            
             
             //Chamada da classe para Desserializar o content Json
            _desser = new DesserializarJson();

            //Chamada do Metodo ConverterJson passando o content
            var item = _desser.ConverterJson(content); 

            ConvertToPlanilha plan = new ConvertToPlanilha();
            plan.ConverterItemtoPlanilha(item);
        }	
    }

    public async Task ConsultaNmRegistroList(List<teste> registros){

        using (HttpClient client = new HttpClient())
        {    
            //Adiciona o Guest ao Header da API      
            client.DefaultRequestHeaders.Add("Authorization" ,"Guest");            
            List<string> nm = new List<string>();
            foreach(var itens in registros)
            {            
                //URL para chamar o metodo GET de consulta da API ANVISA 
                string url = $"https://consultas.anvisa.gov.br/api/consulta/genericos?count=10&filter%5BnumeroRegistro%5D={itens.nome}&page=1";

                //Resposta da API
                var response = await client.GetAsync(url);

                //Converte o response Json em um content string
                var content = await response.Content.ReadAsStringAsync();               
                
                nm.Add(content);

                Console.WriteLine(nm.Count.ToString());
                //Chamada da classe para Desserializar o content Json
                //_desser = new DesserializarJson();

                //Chamada do Metodo ConverterJson passando o content
                //var item = _desser.ConverterJson(content); 

                //ConvertToPlanilha plan = new ConvertToPlanilha();
                //plan.ConverterItemtoPlanilha(item);
            }
            
            Console.WriteLine(nm.Count.ToString());
        }	
    }
}