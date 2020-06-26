using System.ComponentModel.DataAnnotations;

namespace Cep_Consult.Models {
    public class CEP {
        [Required(ErrorMessage = "O campo deve ser preenchido")]
        [MinLength(8, ErrorMessage = "O campo deve ter 8 dígitos")]
        [MaxLength(8, ErrorMessage = "O campo deve ter 8 dígitos")]
        [Display(Name = "Número do Cep:")]
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public string Unidade { get; set; }
        public string Ibge { get; set; }
        public string gia { get; set; }
        public bool Erro { get; set; }
    }
}