using OfficeOpenXml; 
using RoboRegisAPI.Model;

namespace RoboRegisAPI.Services;
public class ServicePlanilha
{
    List<string> registros;
    public ServicePlanilha(){
        registros = new List<string>();
    }
    public List<string> TranformarList()
    {
        try
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var caminho=@"C:\TesteRobo\sheet\reg.xlsx";
            var ep = new ExcelPackage(new FileInfo(caminho));
            var worksheet = ep.Workbook.Worksheets["Registros_Geral"];
        
            for (int rw = 2; rw <= worksheet.Dimension.End.Row; rw++)
            {
                if (worksheet.Cells[rw, 2].Value != null)
                registros.Add(worksheet.Cells[rw, 2].Value.ToString());
            }

        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return registros;
    }
     public void ConverterItemtoPlanilhaList(List<Produtos> item)
    {
        try
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Registros");

                // Cabeçalhos
                CabecalhoExcel(worksheet);

                // Preencher os dados            
                PreencherCelulas(item, worksheet);

                // Salvar a planilha no arquivo
                SalvarPlanilha(package);
            }
            
            Console.WriteLine("Planilha criada com sucesso.");
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    private static void SalvarPlanilha(ExcelPackage package)
    {
        try
        {
            byte[] bin = package.GetAsByteArray();
            File.WriteAllBytes($"RegistrosEXCEL.xlsx", bin);

        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    private static void PreencherCelulas(List<Produtos> item, ExcelWorksheet worksheet)
    {
        try
        {
            for (int i = 0; i < item.Count; i++)
            {
                var itens = item[i]; 
                worksheet.Cells[i + 2, 1].Value = Convert.ToInt32(i+1);
                worksheet.Cells[i + 2, 2].Value = itens.nomeProduto;
                worksheet.Cells[i + 2, 3].Value = Convert.ToInt64(itens.processo);
                worksheet.Cells[i + 2, 4].Value = Convert.ToInt64(itens.registro);
                worksheet.Cells[i + 2, 5].Value = itens.razaoSocial;
                worksheet.Cells[i + 2, 6].Value = itens.cnpj;
                worksheet.Cells[i + 2, 7].Value = Convert.ToInt32(itens.situacao);
                worksheet.Cells[i + 2, 8].Value = Convert.ToDateTime(itens.dataVencimento).ToString("d");
                worksheet.Cells[i + 2, 9].Value = Convert.ToInt32(itens.codigoTipo);
                worksheet.Cells[i + 2, 10].Value = itens.descSituacao;
                worksheet.Cells[i + 2, 11].Value = itens.descTipo;
            }

        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    private static void CabecalhoExcel(ExcelWorksheet worksheet)
    {   
        try
        {
            worksheet.Cells[1, 1].Value = "ID Produto";
            worksheet.Cells[1, 2].Value = "Nome do Produto";
            worksheet.Cells[1, 3].Value = "Processo";
            worksheet.Cells[1, 4].Value = "Registro";
            worksheet.Cells[1, 5].Value = "Razão Social";
            worksheet.Cells[1, 6].Value = "CNPJ";
            worksheet.Cells[1, 7].Value = "Situação";
            worksheet.Cells[1, 8].Value = "Data de Vencimento";
            worksheet.Cells[1, 9].Value = "Código do Tipo";
            worksheet.Cells[1, 10].Value = "Descrição da Situação";
            worksheet.Cells[1, 11].Value = "Descrição do Tipo";

        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}