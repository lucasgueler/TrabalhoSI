using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaMarmoreGranito.Models
{
    public class Chapa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O bloco de origem é obrigatório")]
        public int BlocoId { get; set; }

        [ForeignKey("BlocoId")]
        public Bloco Bloco { get; set; }

        [Required(ErrorMessage = "O tipo de material é obrigatório")]
        [StringLength(50)]
        public string TipoMaterial { get; set; }

        [Required(ErrorMessage = "A largura é obrigatória")]
        public decimal Largura { get; set; }

        [Required(ErrorMessage = "A altura é obrigatória")]
        public decimal Altura { get; set; }

        [Required(ErrorMessage = "A espessura é obrigatória")]
        public decimal Espessura { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório")]
        public decimal Valor { get; set; }

        public bool Ativo { get; set; } = true;
    }
} 