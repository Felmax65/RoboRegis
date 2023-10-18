using OfficeOpenXml; 

namespace RoboRegisAPI.Services;
public class ServicePlanilha{

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
        
            for (int rw = 1; rw <= worksheet.Dimension.End.Row; rw++)
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
}