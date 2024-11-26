using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PIM.Models
{
    public class Usuario
    {
        [Key] // Define como chave primária
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Senha { get; set; }

        public string Endereco { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string UF { get; set; }
        
        [DisplayName("Telefone")]
        public string Numero { get; set; }
        public string CEP { get; set; }
    }
}
