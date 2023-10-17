using RoboRegisAPI.Model;
namespace RoboRegisAPI.Service;

public class ConsumindoAPI
{
    public DesserializarJson _desser;
    
    public async Task ConsultaNmRegistro(string Nregistro){

        using (HttpClient client = new HttpClient())
        {          
            client.DefaultRequestHeaders.Add("Authorization" ,"Guest");            
            
            string url = $"https://consultas.anvisa.gov.br/api/consulta/saude?count=10&filter%5BnumeroRegistro%5D={Nregistro}&page=1";
            var response = await client.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();
            
            Console.WriteLine(content);
        }
	
    }

    public async Task ConsultaNmRegistro2(string Nregistro){

        using (HttpClient client = new HttpClient())
        {          
            client.DefaultRequestHeaders.Add("Authorization" ,"Guest");            
            
            string url = $"https://consultas.anvisa.gov.br/api/consulta/genericos?count=10&filter%5BnumeroRegistro%5D={Nregistro}&page=1";
            var response = await client.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();            
             
            _desser = new DesserializarJson();
            _desser.ConverterJson(content); 
        }
	
    }
}