namespace ProjetoHELIOS_API.Models
{
public class LogEvento
{
public int Id { get; set; }

    public string TipoEvento { get; set; } =
        string.Empty;

    public string Descricao { get; set; } =
        string.Empty;

    public DateTime DataHoraEvento { get; set; } =
        DateTime.Now;

    public string? OrigemEvento { get; set; }

    public string? NivelEvento { get; set; }
}

}