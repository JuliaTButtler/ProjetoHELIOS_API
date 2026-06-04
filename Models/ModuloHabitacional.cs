using System.Text.Json.Serialization;

namespace ProjetoHELIOS_API.Models
{
public class ModuloHabitacional
{
public int Id { get; set; }

    public int HabitatId { get; set; }

    public string NomeModulo { get; set; } =
        string.Empty;

    public string TipoModulo { get; set; } =
        string.Empty;

    public int CapacidadeOcupantes { get; set; }

    public int OcupacaoAtual { get; set; } = 0;

    public string StatusModulo { get; set; } =
        string.Empty;

    public string NivelRisco { get; set; } =
        string.Empty;

    public decimal? IndiceRisco { get; set; }


    // Relacionamentos

    [JsonIgnore]
    public Habitat? Habitat { get; set; }


    [JsonIgnore]
    public List<Reserva> Reservas { get; set; }
        = new();


    [JsonIgnore]
    public List<Sensor> Sensores { get; set; }
        = new();


    [JsonIgnore]
    public List<Alerta> Alertas { get; set; }
        = new();
}

}