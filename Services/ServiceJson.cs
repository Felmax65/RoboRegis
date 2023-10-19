using RoboRegisAPI.Model;
using Newtonsoft.Json;

namespace RoboRegisAPI.Services;
public class ServiceJson
{
    private ProdutosContent _produtos;
    private ServiceCSV _serviceCSV;
    private List<Produtos> _items;
    public ServiceJson()
    {
        _produtos = new ProdutosContent();   
        _items = new List<Produtos>();
        _serviceCSV = new ServiceCSV();
    } 
    public List<Produtos> DesserializarJson(List<string> registros)
    {   
        try
        {
            //Declaracao Tipo Root
            _produtos = new ProdutosContent();

            //Declaracao List Items por Injecao
            _items = new List<Produtos>();

            //Conversao de Json para List do tipo Generico
            foreach (var item in registros) 
            {
                _produtos = JsonConvert.DeserializeObject<ProdutosContent>(item)!;
                _items.AddRange(_produtos.content);
            }       

        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return _items;
    }
    public List<Produtos> DesserializarJson2(string registros)
    {   
        try
        {
            //Declaracao Tipo Root
            _produtos = new ProdutosContent();

            //Declaracao List Items por Injecao
            _items = new List<Produtos>();

            //Conversao de Json para List do tipo Generico            
            _produtos = JsonConvert.DeserializeObject<ProdutosContent>(registros)!;
            _items.AddRange(_produtos.content);                  

        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        System.Console.WriteLine(_items);
        return _items;
    }
     public string CombinarJson(List<ProdutosContent> contents)
    {
        string combinedJson;
        ProdutosContent combinedResponse = new ProdutosContent
        {
            content = contents.SelectMany(resp => resp.content).ToList(),
            totalPages = contents.Count,
            totalElements = contents.Sum(resp => resp.content.Count),
            last = true,
            numberOfElements = contents.Sum(resp => resp.content.Count),
            first = true,
            sort = null,
            size = 10,
            number = 0
        };

        combinedJson = JsonConvert.SerializeObject(combinedResponse);
        return combinedJson;
    }
    public void DesseriaizarRespostas(List<ProdutosContent> contents, string respostas)
    {
        ProdutosContent resposta = JsonConvert.DeserializeObject<ProdutosContent>(respostas);
        contents.Add(resposta);
    }

    public void GerarArquivoJson(string json)
    {        
        var fileName = @"criado.json";
        var jsonString = JsonConvert.SerializeObject(json);
        File.WriteAllText(fileName, jsonString);      

    }
    public void LerArquivoJson()
    {
        string value = File.ReadAllText(@"criado.json");
        ProdutosContent b = new ProdutosContent();
        b = JsonConvert.DeserializeObject<ProdutosContent>(value)!;
        List<Produtos> _items = new List<Produtos>();
        _items.AddRange(b.content);        

        _serviceCSV.TrasformarCSV(_items);
    }
}
