using System.Text.Json.Serialization;

namespace ProjetoHELIOS_API.Models
{
public class Ocupante
{
public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Funcao { get; set; }

    public string StatusOcupante { get; set; } = string.Empty;

    public DateTime DataRegistro { get; set; } = DateTime.Now;

    // Relacionamentos

    [JsonIgnore]
    public List<Reserva> Reservas { get; set; } = new();
}

}