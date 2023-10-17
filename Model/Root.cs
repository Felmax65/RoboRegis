namespace RoboRegisAPI.Model;
public class Root
{
    public List<Genericos> content { get; set; }
    public int totalElements { get; set; }
    public int totalPages { get; set; }
    public bool last { get; set; }
    public int numberOfElements { get; set; }
    public bool first { get; set; }
    public object sort { get; set; }
    public int size { get; set; }
    public int number { get; set; }
}