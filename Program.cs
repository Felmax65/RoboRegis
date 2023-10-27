// See https://aka.ms/new-console-template for more information
using RRegis.Service;

ServiceConsulta sc = new ServiceConsulta();

await sc.Consulta();

Console.ReadKey();
