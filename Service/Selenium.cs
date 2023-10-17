using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RoboRegisApi.Model;

namespace RoboRegisAPI.Service;

public class Selenium{

    private string Url { get; set; }
    private string Path { get; set; }
    private IWebDriver Driver; 
    private Produto Produto; 

    public Selenium()
    {
        Path = @"C:\TesteRobo\chromedriver\chromedriver.exe";
        Url = "https://intoli.com/blog/making-chrome-headless-undetectable/chrome-headless-test.html";
        Driver = new ChromeDriver(Path);
        Produto = new Produto();

    }

    private void HabilitaSelenium()
    {       
       Driver.Navigate().GoToUrl(Url);
       Driver.Navigate().Refresh();
       
    }

    public void ConsultarReg()
    {
       HabilitaSelenium();
    }

}