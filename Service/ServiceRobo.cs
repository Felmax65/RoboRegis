namespace RRegis.Service;
public class ServiceRobo{
    private ServiceConsulta _serviceConsulta;
    private ServiceJson _serviceJson;
    public ServiceRobo(){        
        _serviceConsulta = new ServiceConsulta();
        _serviceJson = new ServiceJson();
    }
    public async Task<string> ConsultarRegistros(){
        var consultas = await _serviceConsulta.Consulta();
        _serviceJson.Desserializar(consultas); 
        var msg = "OK";
        return msg;              
    }
}