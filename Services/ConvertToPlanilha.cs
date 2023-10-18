using RoboRegisAPI.Model;
using OfficeOpenXml; 

namespace RoboRegisAPI.Services;

public class ConvertToPlanilha
{  

    public void ConverterItemtoPlanilhaList(List<Produtos> item)
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

    private static void SalvarPlanilha(ExcelPackage package)
    {
        byte[] bin = package.GetAsByteArray();

        File.WriteAllBytes("Registro.xlsx", bin);
    }

    private static void PreencherCelulas(List<Produtos> item, ExcelWorksheet worksheet)
    {
        for (int i = 0; i < item.Count; i++)
        {
            var itens = item[i];
            worksheet.Cells[i + 2, 1].Value = itens.nomeProduto;
            worksheet.Cells[i + 2, 2].Value = itens.processo;
            worksheet.Cells[i + 2, 3].Value = itens.registro;
            worksheet.Cells[i + 2, 4].Value = itens.razaoSocial;
            worksheet.Cells[i + 2, 5].Value = itens.cnpj;
            worksheet.Cells[i + 2, 6].Value = itens.situacao;
            worksheet.Cells[i + 2, 7].Value = itens.dataVencimento;
            worksheet.Cells[i + 2, 8].Value = itens.codigoTipo;
            worksheet.Cells[i + 2, 9].Value = itens.descSituacao;
            worksheet.Cells[i + 2, 10].Value = itens.descTipo;
        }
    }

    private static void CabecalhoExcel(ExcelWorksheet worksheet)
    {
        worksheet.Cells[1, 1].Value = "Nome do Produto";
        worksheet.Cells[1, 2].Value = "Processo";
        worksheet.Cells[1, 3].Value = "Registro";
        worksheet.Cells[1, 4].Value = "Razão Social";
        worksheet.Cells[1, 5].Value = "CNPJ";
        worksheet.Cells[1, 6].Value = "Situação";
        worksheet.Cells[1, 7].Value = "Data de Vencimento";
        worksheet.Cells[1, 8].Value = "Código do Tipo";
        worksheet.Cells[1, 9].Value = "Descrição da Situação";
        worksheet.Cells[1, 10].Value = "Descrição do Tipo";
    }
}