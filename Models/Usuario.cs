namespace ProjetoHELIOS_API.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;

        public string TipoUsuario { get; set; } = string.Empty;

        public string StatusUsuario { get; set; } = string.Empty;

        public int NivelAcesso { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}