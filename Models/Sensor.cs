using System.Text.Json.Serialization;

namespace ProjetoHELIOS_API.Models
{
public class Sensor
{
public int Id { get; set; }

    public int ModuloId { get; set; }

    public string NomeSensor { get; set; } = string.Empty;

    public string TipoSensor { get; set; } = string.Empty;

    public string StatusSensor { get; set; } = string.Empty;

    public string UnidadeMedida { get; set; } = string.Empty;

    public decimal? LimiteMinimo { get; set; }

    public decimal? LimiteMaximo { get; set; }

    public int? IntervaloLeituraSegundos { get; set; }

    public DateTime DataInstalacao { get; set; } =
        DateTime.Now;

    // Relacionamentos

    [JsonIgnore]
    public ModuloHabitacional? Modulo { get; set; }

    [JsonIgnore]
    public List<LeituraSensor> Leituras { get; set; } = new();

    [JsonIgnore]
    public List<Alerta> Alertas { get; set; } = new();
}

}