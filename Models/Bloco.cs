using System.ComponentModel.DataAnnotations;

namespace SistemaMarmoreGranito.Models
{
    public class Bloco
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O código do bloco é obrigatório")]
        [StringLength(50)]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "A pedreira de origem é obrigatória")]
        [StringLength(100)]
        public string PedreiraOrigem { get; set; }

        [Required(ErrorMessage = "A metragem é obrigatória")]
        public decimal MetragemM3 { get; set; }

        [Required(ErrorMessage = "O tipo de material é obrigatório")]
        [StringLength(50)]
        public string TipoMaterial { get; set; }

        [Required(ErrorMessage = "O valor de compra é obrigatório")]
        public decimal ValorCompra { get; set; }

        [Required(ErrorMessage = "O número da nota fiscal é obrigatório")]
        [StringLength(50)]
        public string NumeroNotaFiscal { get; set; }

        public bool Ativo { get; set; } = true;
    }
} 