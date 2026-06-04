using System.Text.Json.Serialization;

namespace ProjetoHELIOS_API.Models
{
public class Reserva
{
public int Id { get; set; }

    public int OcupanteId { get; set; }

    public int ModuloId { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime? DataFim { get; set; }

    public string StatusReserva { get; set; } = string.Empty;

    // Relacionamentos

    [JsonIgnore]
    public Ocupante? Ocupante { get; set; }

    [JsonIgnore]
    public ModuloHabitacional? Modulo { get; set; }
}

}