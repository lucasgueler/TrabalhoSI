using System.ComponentModel.DataAnnotations;

namespace SistemaMarmoreGranito.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O login é obrigatório")]
        [StringLength(50)]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(100)]
        public string Senha { get; set; }

        public bool Ativo { get; set; } = true;
    }
} 