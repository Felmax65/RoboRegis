using RoboRegisAPI.Model;

namespace RoboRegisAPI.Services;

public class ServiceCSV{

    public async void TrasformarCSV(List<Produtos> item){
        int count = 1;
        using (var escritor = new StreamWriter("registros.csv"))
        {
        foreach (var itens in item) 
            {
                
                Convert.ToDateTime(itens.dataVencimento).ToString("d");
                await escritor.WriteLineAsync($"{count};{itens.registro};{itens.razaoSocial};{itens.dataVencimento};{itens.descSituacao};{itens.descTipo};");
                count++;
            }
        }
    }
}