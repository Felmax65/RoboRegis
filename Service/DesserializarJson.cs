using System.Text.Json;
using RoboRegisAPI.Model;

namespace RoboRegisAPI.Service;

public class DesserializarJson
{
    public List<Genericos> ConverterJson(string content)
    {
        Console.WriteLine(content);
        Root produtos = JsonSerializer.Deserialize<Root>(content)!;

        List<Genericos> items = produtos.content;

        return items;
    }
}