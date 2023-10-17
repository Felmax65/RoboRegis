using RoboRegisAPI.Model;
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
            _desser.ConverterJson(content); 
        }
	
    }
}