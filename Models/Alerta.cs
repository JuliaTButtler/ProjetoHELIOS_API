using System.Text.Json.Serialization;

namespace ProjetoHELIOS_API.Models
{
public class Alerta
{
public int Id { get; set; }

    public int ModuloId { get; set; }

    public int SensorId { get; set; }

    public string TipoAlerta { get; set; } =
        string.Empty;

    public string Mensagem { get; set; } =
        string.Empty;

    public string NivelCriticidade { get; set; } =
        string.Empty;

    public DateTime DataHoraAlerta { get; set; } =
        DateTime.Now;

    public DateTime? DataHoraResolucao { get; set; }

    public string StatusAlerta { get; set; } =
        string.Empty;

    public string? AcaoCorretiva { get; set; }

    // Relacionamentos

    [JsonIgnore]
    public ModuloHabitacional? Modulo { get; set; }

    [JsonIgnore]
    public Sensor? Sensor { get; set; }

    [JsonIgnore]
    public List<AcaoAutomatica> Acoes { get; set; } =
        new();
}

}