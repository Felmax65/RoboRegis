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
    public void GerarPlanilha(List<RootVigente> vigentes , List<RootVencido> vencidos, List<RootCancelado> cancelados){

        try{
            using(var package = new ExcelPackage()){
                
                var data = DateTime.Now.ToString("d");
                var caminho = $"C:\\RoboRegis\\Dados-RoboRegis\\Registros-Anvisa{data}.xlsx";

                if(caminho != null){

                    var vigentesSheet = package.Workbook.Worksheets.Add("Registros_Vigentes");
                    vigentesSheet.Cells.LoadFromCollection(vigentes,true);

                    var vencidosSheet = package.Workbook.Worksheets.Add("Registros_Vencidos");
                    vigentesSheet.Cells.LoadFromCollection(vencidos,true);

                    var canceladosSheet = package.Workbook.Worksheets.Add("Registros_Cancelados");
                    vigentesSheet.Cells.LoadFromCollection(cancelados,true);

                    package.SaveAs(new FileInfo(caminho));
                }
                else{
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(@"Diretorio não existente, verifique se caminho está configurado no computador C:\RoboRegis\Dados-RoboRegis\");
                }
            }
        }
        catch(Exception e){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro ao gerar a planilha: {e.Message}");
        }
    }
}