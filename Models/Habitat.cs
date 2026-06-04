using System.Text.Json.Serialization;

namespace ProjetoHELIOS_API.Models
{
    public class Habitat
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Localizacao { get; set; } = string.Empty;

        public string TipoHabitat { get; set; } = string.Empty;

        public int CapacidadeTotal { get; set; }

        public string StatusOperacional { get; set; } = string.Empty;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        // Relacionamentos

        [JsonIgnore]
        public List<ModuloHabitacional> Modulos { get; set; } = new();
    }
}