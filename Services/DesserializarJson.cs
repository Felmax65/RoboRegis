using System.Text.Json;
using RoboRegisAPI.Model;

namespace RoboRegisAPI.Services;

public class DesserializarJson
{
    public List<Genericos> ConverterJson(string content)
    {    
        //Conversao de Json para tipo Enumerable   
        Root produtos = JsonSerializer.Deserialize<Root>(content)!;

        //Conversao do Enumerable para List
        List<Genericos> items = produtos.content;

        //Retorna uma lista de items
        return items;
    }
}