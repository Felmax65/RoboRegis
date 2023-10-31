using System.Net.Http.Headers;
namespace RRegis.Service;
public class ServiceApi{
    public HttpClient _client;
    private HttpResponseMessage _serviceResponse;
    public ServiceApi(){
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.DefaultRequestHeaders.Add("Authorization","Guest");
        _serviceResponse = new HttpResponseMessage();
    }
     public async Task<string> ConsumirApi(string Url){        
        _serviceResponse = await _client.GetAsync(Url);
        await Task.Delay(5000);
        if(_serviceResponse.IsSuccessStatusCode){
            var content = await _serviceResponse.Content.ReadAsStringAsync();
            return content;   
        }
        else{
            Console.ForegroundColor = ConsoleColor.Red;
            throw new Exception($"Erro na Requisicao: {_serviceResponse.StatusCode}");
        }
    }
}