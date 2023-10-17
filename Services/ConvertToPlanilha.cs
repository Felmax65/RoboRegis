using RoboRegisAPI.Model;
using OfficeOpenXml; 
using OfficeOpenXml.Style; 

namespace RoboRegisAPI.Services;

public class ConvertToPlanilha
{
    public void ConverterItemtoPlanilha(List<Genericos> item)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Registros");

            // Cabeçalhos
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

            // Preencher os dados            
            for (int i = 0; i < item.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = item[i].nomeProduto;
                worksheet.Cells[i + 2, 2].Value = item[i].processo;
                worksheet.Cells[i + 2, 3].Value = item[i].registro;
                worksheet.Cells[i + 2, 4].Value = item[i].razaoSocial;
                worksheet.Cells[i + 2, 5].Value = item[i].cnpj;
                worksheet.Cells[i + 2, 6].Value = item[i].situacao;
                worksheet.Cells[i + 2, 7].Value = item[i].dataVencimento;
                worksheet.Cells[i + 2, 8].Value = item[i].codigoTipo;
                worksheet.Cells[i + 2, 9].Value = item[i].descSituacao;
                worksheet.Cells[i + 2, 10].Value = item[i].descTipo;
            }

            // Salvar a planilha no arquivo
            var fi = new System.IO.FileInfo("MinhaPlanilha.xlsx");
            package.SaveAs(fi);
        }

        Console.WriteLine("Planilha criada com sucesso.");
    }
}