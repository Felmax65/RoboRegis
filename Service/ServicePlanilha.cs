using OfficeOpenXml;
using RRegis.Model;
using RRegis.Model.ModelJson.Vigente;
using RRegis.Model.ModelJson.Vencidos;
using RRegis.Model.ModelJson.Cancelado;

namespace RRegis.Service;
public class ServicePlanilha{
    private List<Registro> _registros;
    public ServicePlanilha(){
       _registros = new List<Registro>();
    }
    public List<Registro> ConverterRegistros(){    
        
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        var caminho = @"C:\RoboRegis\Dados-RoboRegis\Entrada\reg.xlsx";

        if (caminho != null){
            var ep = new ExcelPackage(new FileInfo(caminho));
            var worksheet = ep.Workbook.Worksheets["Registros"];
            var row = worksheet.Dimension.End.Row;

            for (int rw = 2; rw <= row; rw++)
            {
                string registro = worksheet.Cells[rw, 1].Value?.ToString();
                string processo = worksheet.Cells[rw, 2].Value?.ToString();

                if (!string.IsNullOrWhiteSpace(registro)){
                    _registros.Add(new Registro{
                        NmRegistro = registro,
                        NmProcesso = processo
                    }); 
                }   
                
            }
            return _registros;  
        }
        else{
            Console.ForegroundColor = ConsoleColor.Red;
            throw new Exception ($"Algo de errado ocorreu com a planilha");
        }          
    }
    public void GerarPlanilha(List<RootVigente> vigentes){

        try{
            
            var data = DateTime.Now.ToString("d");
            var caminho = @$"C:\RoboRegis\\Dados-RoboRegis\Saida\Registros-{data.Replace("/","")}.xlsx";
            if (caminho != null && vigentes != null){
                using (var package = new ExcelPackage(new FileInfo(caminho))){

                    var vigentesSheet = package.Workbook.Worksheets.Add("Registros_Vigentes");

                    // Adicione as colunas iniciais
                    vigentesSheet.Cells[1, 1].Value = "Produto";
                    vigentesSheet.Cells[1, 2].Value = "Registro";
                    vigentesSheet.Cells[1, 3].Value = "Processo";
                    // Adicione as colunas adicionais                    
                    vigentesSheet.Cells[1, 4].Value = "CNPJ";
                    vigentesSheet.Cells[1, 5].Value = "Razão Social";
                    vigentesSheet.Cells[1, 6].Value = "Cancelado";
                    vigentesSheet.Cells[1, 7].Value = "Data Cancelado";
                    vigentesSheet.Cells[1, 8].Value = "Data Vencimento";
                    vigentesSheet.Cells[1, 9].Value = "Descrição";

                    int row = 2; // Comece a partir da segunda linha

                    foreach (var root in vigentes){
                        // Preencha as colunas iniciais
                        vigentesSheet.Cells[row, 1].Value = root.produto;
                        vigentesSheet.Cells[row, 2].Value = root.registro;
                        vigentesSheet.Cells[row, 3].Value = root.processo;

                        if (root.empresa != null){
                            vigentesSheet.Cells[row, 4].Value = root.empresa.cnpj;
                            vigentesSheet.Cells[row, 5].Value = root.empresa.razaoSocial;
                        }

                        vigentesSheet.Cells[row, 6].Value = root.cancelado;
                        vigentesSheet.Cells[row, 7].Value = root.dataCancelamento;

                        if (root.vencimento != null){
                            vigentesSheet.Cells[row, 8].Value = root.vencimento.data;
                            vigentesSheet.Cells[row, 9].Value = root.vencimento.descricao;
                        }

                        row++; // Avance para a próxima linha
                    }

                    package.Save();            
                }   
            }
        }
        catch(Exception e){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro ao gerar a planilha: {e.Message}");
        }
    }
}