namespace ProjetoHELIOS_API.Models
{
public class RegraAlerta
{
public int Id { get; set; }

    public string TipoSensor { get; set; } =
        string.Empty;

    public decimal? ValorMinimo { get; set; }

    public decimal? ValorMaximo { get; set; }

    public string NivelCriticidade { get; set; } =
        string.Empty;

    public int PesoRisco { get; set; }

    public string MensagemPadrao { get; set; } =
        string.Empty;

    public string Ativo { get; set; } = "S";
}

}