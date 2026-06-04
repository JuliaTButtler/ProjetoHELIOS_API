using System.Text.Json.Serialization;

namespace ProjetoHELIOS_API.Models
{
public class AcaoAutomatica
{
public int Id { get; set; }

    public int AlertaId { get; set; }

    public string Descricao { get; set; } =
        string.Empty;

    public DateTime DataHoraExecucao { get; set; } =
        DateTime.Now;

    public string StatusAcao { get; set; } =
        string.Empty;

    // Relacionamentos

    [JsonIgnore]
    public Alerta? Alerta { get; set; }
}

}