
using System;
using System.Collections.Generic;

namespace RoboRegisApi.ModelJson.Vigentes;
public class Empresa
{
    public string Cnpj { get; set; }
    public string RazaoSocial { get; set; }
    public string Autorizacao { get; set; }
}

public class Mensagem
{
    public object Situacao { get; set; }
    public object Resolucao { get; set; }
    public object Motivo { get; set; }
    public bool Negativo { get; set; }
}

public class Apresentacao
{
    public string Modelo { get; set; }
    public object Componente { get; set; }
    public object Apresentacaos { get; set; }
}

public class Fabricante
{
    public string Atividade { get; set; }
    public string RazaoSocial { get; set; }
    public string Pais { get; set; }
    public object Local { get; set; }
}

public class Risco
{
    public string Sigla { get; set; }
    public string Descricao { get; set; }
}

public class Vencimento
{
    public object Data { get; set; }
    public string Descricao { get; set; }
}

public class Arquivo
{
    public string AnexoCod { get; set; }
    public string NuExpediente { get; set; }
    public string NomeArquivo { get; set; }
    public int TipoAnexo { get; set; }
    public string TipoArquivo { get; set; }
    public object DtEnvio { get; set; }
    public string NuProcesso { get; set; }
    public string DescricaoTipoAnexo { get; set; }
    public string NomeCompleto { get; set; }
}

public class RootVigentes
{
    public string Produto { get; set; }
    public Empresa Empresa { get; set; }
    public Mensagem Mensagem { get; set; }
    public string NomeTecnico { get; set; }
    public string Registro { get; set; }
    public bool Cancelado { get; set; }
    public object DataCancelamento { get; set; }
    public string Processo { get; set; }
    public List<Apresentacao> Apresentacoes { get; set; }
    public List<Fabricante> Fabricantes { get; set; }
    public Risco Risco { get; set; }
    public Vencimento Vencimento { get; set; }
    public object Publicacao { get; set; }
    public bool ApresentacaoModelo { get; set; }
    public List<Arquivo> Arquivos { get; set; }
    public object ProcessoMedidaCautelar { get; set; }
    public string Tooltip { get; set; }
}
