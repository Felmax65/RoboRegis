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

    #region PROCESSO DE TRATAMENTO DE JSON
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
    public void DesserializarRespostas(List<ProdutosContent> contents, string respostas)
    {
        /**
            Desserializa a string de respostas para o tipo ProdutoContent facilitando leitura a conversão para Classe Produtos
        **/
        ProdutosContent resposta = JsonConvert.DeserializeObject<ProdutosContent>(respostas)!;
        contents.Add(resposta);
    }
    #endregion

    #region TESTE PARA GERAR CSV

    public string CombinarJson(List<ProdutosContent> contents)
    {
        /**
            Metodo Responsavel por combinar todas as respostas de um List Produto Contentes e transformar a string em json com array de registros
        **/
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
    public List<Produtos> DesserializarJson(string registros)
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

    public void GerarArquivoJson(string json)
    {   
        /**
            Metodo resposavel por gerar um arquivo .json a partir de uma string contendo todos as respostas e contents dos registros
        **/     
        var fileName = @"criado.json";
        json = json.Replace("\\","");
        File.WriteAllText(fileName,json);

    }
    public void LerArquivoJson()
    {
        /**
            Metodo Responsavel por Ler e transformar um Arquivo Json para CSV
        **/
        string value = File.ReadAllText(@"criado.json");
        ProdutosContent b = new ProdutosContent();
        b = JsonConvert.DeserializeObject<ProdutosContent>(value)!;
        List<Produtos> _items = new List<Produtos>();
        _items.AddRange(b.content);        

        _serviceCSV.TrasformarCSV(_items);
    }
    #endregion
}