using RoboRegisAPI.Model;

namespace RoboRegisAPI.Services;
public class ServiceCSV
{
    public void TrasformarCSV(List<Produtos> item)
    {
        try
        {
            int count = 1;
            using (var escritor = new StreamWriter("Registros-CSV.csv"))
            {
            foreach (var itens in item) 
                {                
                    Convert.ToDateTime(itens.dataVencimento).ToString("d");
                    escritor.WriteLine($"{count};{itens.registro};{itens.razaoSocial};{itens.dataVencimento};{itens.descSituacao};{itens.descTipo};");
                    count++;
                }
            }
            System.Console.WriteLine("CSV Criado");
        }
        catch(Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }
    
}