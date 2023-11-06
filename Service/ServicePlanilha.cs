using System.Runtime.CompilerServices;
using System.Xml.XPath;
using OfficeOpenXml;
using RRegis.Model;
using RRegis.Model.ModelJson.Vigente;

namespace RRegis.Service;
public class ServicePlanilha{
    private List<Registro> _registros;
    public ServicePlanilha(){
       _registros = new List<Registro>();
    }

    #region ConverterRegistros
    //Metodo Responsavel por transformar a planilha de entrada em um LIST
    public List<Registro> ConverterRegistros(){    
        
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Atribui o tipo de licença da planilha
        var caminho = @"C:\RoboRegis\Dados-RoboRegis\Entrada\reg.xlsx";// Caminho da planilha de entrada

        if (caminho != null){ //verificacao de caminho nulo
            var ep = new ExcelPackage(new FileInfo(caminho)); //Iniciliazação para leitura da planilha
            var worksheet = ep.Workbook.Worksheets["Registros"]; // Planilha e aba para realizacao da leitura
            var row = worksheet.Dimension.End.Row; // Quantidade maxima de linhas no arquivo

            for (int rw = 2; rw <= row; rw++)
            {
                string registro = worksheet.Cells[rw, 1].Value?.ToString().Trim(); // Faz a leitura da primeira linha da coluna 1 
                string processo = worksheet.Cells[rw, 2].Value?.ToString().Trim(); // Faz a leitura da primeira linha da coluna 2  

                if (!string.IsNullOrWhiteSpace(registro)){ // Verifica se há espaços nulos ou em branco na planilha
                    _registros.Add(new Registro{
                        NmRegistro = registro,
                        NmProcesso = processo
                    }); // Converte os dados das 2 colunas em um LIST de acordo com a classe Generica Registro
                }   
                
            }
            return _registros;  // retorna o List do tipo Registros
        }
        else{
            Console.ForegroundColor = ConsoleColor.Red; 
            throw new Exception ($"-Algo de errado ocorreu com a planilha");
        }          
    }
    #endregion
   
    #region GerarPlanilha
    //Metodo Responsavel por gerar a planilha apartir o List da classe generica RootVigente
    public void GerarPlanilha(List<RootVigente> vigentes){

        try{
            
            var data = DateTime.Now.ToString("d");
            var data1 = DateTime.Now;
            var caminho = @$"C:\RoboRegis\\Dados-RoboRegis\Saida\Registros-{data.Replace("/","")}.xlsx"; //Caminho para salvar a planilha
            if (caminho != null && vigentes != null){ //Verificação de nulo
                using (var package = new ExcelPackage(new FileInfo(caminho)))
                {

                    var vigentesSheet = package.Workbook.Worksheets.Add("Registros_Vigentes");

                    PreencherCabecalho(vigentesSheet); // Cabelçalho da planilha

                    int row = 2; // Começa a partir da segunda linha

                    row = PreencherColunas(vigentes, data1, vigentesSheet, row); //Metodo para preencher as colunas

                    package.Save(); //Salva a planilha
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("-Planilha gerada com sucesso em C:\\RoboRegis\\Dados-RoboRegis\\Saida\\");
                }
            }
        }
        catch(Exception e){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"-Erro ao gerar a planilha: {e.Message}");
        }
    }

    private static void PreencherCabecalho(ExcelWorksheet vigentesSheet)
    {
        // Adiciona as colunas iniciais
        vigentesSheet.Cells[1, 1].Value = "PRODUTO"; // Coluna 1
        vigentesSheet.Cells[1, 2].Value = "REGISTRO"; // Coluna 2
        vigentesSheet.Cells[1, 3].Value = "PROCESSO"; // Coluna 3
        // Adiciona as colunas adicionais                    
        vigentesSheet.Cells[1, 4].Value = "CNPJ"; //Coluna 4
        vigentesSheet.Cells[1, 5].Value = "RAZÃO SOCIAL";//Coluna 5
        vigentesSheet.Cells[1, 6].Value = "STATUS";//Coluna 6
        vigentesSheet.Cells[1, 7].Value = "DATA";//Coluna 7
    }

    private static int PreencherColunas(List<RootVigente> vigentes, DateTime data1, ExcelWorksheet vigentesSheet, int row)
    {
        foreach (var root in vigentes)
        {
            // Preenche as colunas iniciais
            vigentesSheet.Cells[row, 1].Value = root.produto; // Insere o nome do Produto Coluna 1
            vigentesSheet.Cells[row, 2].Value = root.registro; // insere o numero do Registro Coluna 2
            vigentesSheet.Cells[row, 3].Value = root.processo; // Insere o numero do Processo Coluna 3

            if (root.empresa != null) // Verifica se nome da empresa é diferente de nulo
            {
                vigentesSheet.Cells[row, 4].Value = root.empresa.cnpj; // Insere o nome da emoresa na coluna 4
                vigentesSheet.Cells[row, 5].Value = root.empresa.razaoSocial; // Insere a razão socual da empresa na coluna 5
            }

            if (root.cancelado == "true") // Verifica se a variavel cancelado é igual a true
            {
                vigentesSheet.Cells[row, 6].Value = root.cancelado = "CANCELADO"; // Insere o status "CANCELADO" na coluna STATUS coluna 6
                vigentesSheet.Cells[row, 7].Value = Convert.ToDateTime(root.dataCancelamento).ToString("d"); // Insere a data de cancelamento na coluna 7
            }

            if (root.vencimento != null) // Verifica se vencimento é diferente de nulo
            {
                vigentesSheet.Cells[row, 6].Value = root.vencimento.descricao; // Insere o Status VIGENTE na coluna STATUS 

                if (root.vencimento.data != null) // Verifica se data é diferente de nulo
                {

                    var dtCompara = DateTime.Compare(Convert.ToDateTime(root.vencimento.data), data1); // Compara se a data Vencimento é maior do que a data Atual

                    if (dtCompara > 0) // Se data vencimento maior que 0
                    {
                        vigentesSheet.Cells[row, 6].Value = "VENCE EM"; // Insere  VENCE EM na coluna STATUS 
                        vigentesSheet.Cells[row, 7].Value = Convert.ToDateTime(root.vencimento.data).ToString("d"); // Insere a data na coluna 7
                    }
                    else
                    {
                        vigentesSheet.Cells[row, 6].Value = "VENCIDO";// Insere  VENCIDO na coluna STATUS 
                        vigentesSheet.Cells[row, 7].Value = Convert.ToDateTime(root.vencimento.data).ToString("d");// Insere a data na coluna 7
                    }
                }
            }

            row++; // Avança para a próxima linha
        }

        return row;
    }
    #endregion
}