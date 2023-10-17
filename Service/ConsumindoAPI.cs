namespace RoboRegisAPI.Service;

public static class ConsumindoAPI
{
    public static async Task ConsultaNmRegistro(string Nregistro){

        using (HttpClient client = new HttpClient())
        {          
            client.DefaultRequestHeaders.Add("Authorization" ,"Guest");            
            
            string url = $"https://consultas.anvisa.gov.br/api/consulta/saude?count=10&filter%5BnumeroRegistro%5D={Nregistro}&page=1";
            var response = await client.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(content);
        }
	
    }
}