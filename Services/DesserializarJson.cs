using System.Text.Json;
using RoboRegisAPI.Model;

namespace RoboRegisAPI.Services;

public class DesserializarJson
{
    private ProdutosContent _produtos;
    private List<Produtos> _items;
    public DesserializarJson()
    {
        _produtos = new ProdutosContent();   
        _items = new List<Produtos>();
    } 

    public List<Produtos> ConverterParaJson(List<string> registros)
    {
        //Declaracao Tipo Root
        _produtos = new ProdutosContent();

        //Declaracao List Items por Injecao
        _items = new List<Produtos>();

        //Conversao de Json para List do tipo Generico
        foreach (var item in registros) 
        {
            _produtos = JsonSerializer.Deserialize<ProdutosContent>(item)!;
            _items.AddRange(_produtos.content);
        }       
        return _items;
    }
}