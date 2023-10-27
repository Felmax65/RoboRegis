namespace RRegis.Model.ModelJson.Vencidos;

public class Empresa{
    public string cnpj { get; set; }
    public string razaoSocial { get; set; }
    public string autorizacao { get; set; }
}
public class Mensagem{
    public object situacao { get; set; }
    public object resolucao { get; set; }
    public object motivo { get; set; }
    public bool negativo { get; set; }
}
public class Apresentacao{
    public string modelo { get; set; }
    public object componente { get; set; }
    public object apresentacao { get; set; }
}
public class Fabricante{
    public string atividade { get; set; }
    public string razaoSocial { get; set; }
    public string pais { get; set; }
    public object local { get; set; }
}
public class Risco{
    public string sigla { get; set; }
    public string descricao { get; set; }
}
public class Vencimento{
    public DateTime? data { get; set; }
    public object descricao { get; set; }
}
public class Arquivo{
    public string anexoCod { get; set; }
    public string nuExpediente { get; set; }
    public string nomeArquivo { get; set; }
    public int tipoAnexo { get; set; }
    public string tipoArquivo { get; set; }
    public DateTime dtEnvio { get; set; }
    public string nuProcesso { get; set; }
    public string descricaoTipoAnexo { get; set; }
    public string nomeCompleto { get; set; }
}

public class RootVencido{
    public string produto { get; set; }
    public Empresa empresa { get; set; }
    public Mensagem mensagem { get; set; }
    public string nomeTecnico { get; set; }
    public string registro { get; set; }
    public bool cancelado { get; set; }
    public object dataCancelamento { get; set; }
    public string processo { get; set; }
    public List<Apresentacao> apresentacoes { get; set; }
    public List<Fabricante> fabricantes { get; set; }
    public Risco risco { get; set; }
    public Vencimento vencimento { get; set; }
    public object publicacao { get; set; }
    public bool apresentacaoModelo { get; set; }
    public List<Arquivo> arquivos { get; set; }
    public object processoMedidaCautelar { get; set; }
    public string tooltip { get; set; }
}