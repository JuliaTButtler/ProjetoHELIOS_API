using System.Text.Json.Serialization;

namespace ProjetoHELIOS_API.Models
{
public class LeituraSensor
{
public int Id { get; set; }

    public int SensorId { get; set; }

    public decimal ValorLeitura { get; set; }

    public DateTime DataHoraLeitura { get; set; } =
        DateTime.Now;

    public string StatusLeitura { get; set; } =
        string.Empty;

    // Relacionamentos

    [JsonIgnore]
    public Sensor? Sensor { get; set; }
}

}