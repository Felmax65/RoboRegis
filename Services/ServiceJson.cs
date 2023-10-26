using RoboRegisApi.Model;
using Newtonsoft.Json;
using RoboRegisApi.ModelJson.Cancelados;
using RoboRegisApi.ModelJson.Vencidos;
using RoboRegisApi.ModelJson.Vigentes;

namespace RoboRegisApi.Services;
public class ServiceJson
{
    private ProdutosContent _produtos;
    private ServicePlanilha _servicePlanilha;
    private List<Produtos> _items;
    public ServiceJson()
    {
        _produtos = new ProdutosContent();   
        _items = new List<Produtos>();
        _servicePlanilha = new ServicePlanilha();
    } 
    public List<Produtos> DesserializarJson(List<string> registros)
    {   
        try
        {
            //Declaracao Tipo Root
            _produtos = new ProdutosContent();

            //Declaracao List Items por Injecao
            _items = new List<Produtos>();
            
            if(registros != null){
            //Conversao de Json para List do tipo Generico
                foreach (var item in registros) 
                {
                    _produtos = JsonConvert.DeserializeObject<ProdutosContent>(item)!;
                    _items.AddRange(_produtos.content);
                }    
            }
            else{
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("-Lista de produtos em branco");               
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

    #region NOVO TESTE

    public void DesserializarNOVO(List<string> registros)
    {   
        List<RootVigentes> rootVigentes = new List<RootVigentes>();
        List<RootVencidos> rootVencidos = new List<RootVencidos>();
        List<RootCancelado> rootCancelados = new List<RootCancelado>();

      
        foreach(var jsons in registros){
            
            
            var jsona = JsonConvert.DeserializeObject<RootVigentes>(jsons);
            if(jsona != null){
                rootVigentes.Add(jsona);
                _servicePlanilha.ConverterItemtoPlanilhaVigentes(rootVigentes);
            }         
        }
    }    

    #endregion
}