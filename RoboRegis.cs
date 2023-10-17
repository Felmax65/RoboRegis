using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RoboRegisAPI.Service;

namespace RoboRegisAPI;
class RoboRegis
{
    static void Main(string[] args)
    {
       Selenium selenium = new Selenium();
       selenium.ConsultarReg();        
    }
}