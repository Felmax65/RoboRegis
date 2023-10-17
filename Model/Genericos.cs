using System.Text.Json;
using System.Text.Json.Serialization;

namespace RoboRegisAPI.Model;
public class Genericos
{
  public string nomeProduto { get; set; }
    public string processo { get; set; }
    public string registro { get; set; }
    public string razaoSocial { get; set; }
    public string cnpj { get; set; }
    public int situacao { get; set; }
    public  string dataVencimento { get; set; }    
    public int codigoTipo { get; set; }
    public string descSituacao { get; set; }
    public string descTipo { get; set; }
}