using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ImobiliariaVirtual.Models
{
    public class Imovel
    {
        public string Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "O tipo não deve ser maior que 50 caracteres")]
        public string Tipo { get; set; }
        [DisplayName("Valor de Venda")]
        [Range(1, 100000000, ErrorMessage = "O valor de venda deve ser entre 1 e 100M")]
        public float ValorVenda { get; set; }
        [DisplayName("Valor de Locação")]
        [Range(1, 500000, ErrorMessage = "O valor de locação deve ser entre 1 e 500k")]
        public float ValorLocacao { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "O endereco não deve ser maior que 50 caracteres")]
        [DisplayName("Endereço")]
        public string Endereco { get; set; }
        [Required]
        [DisplayName("Número")]
        public int Numero { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O complemento não deve ser maior que 100 caracteres")]
        public string Complemento { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "O bairro não deve ser maior que 50 caracteres")]
        public string Bairro { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "A cidade não deve ser maior que 50 caracteres")]
        public string Cidade { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        [StringLength(8, ErrorMessage = "O CEP não deve ser maior que 8 caracteres (somente os dígitos)")]
        public string CEP { get; set; }
    }
}
