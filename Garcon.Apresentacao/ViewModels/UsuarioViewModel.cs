using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Garcom.Apresentacao.ViewModels
{
    public class UsuarioViewModel
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Preencha o nome!")]
        [MaxLength(20, ErrorMessage = "O tamanho máximo é {0}")]
        [MinLength(2, ErrorMessage = "O tamanho mínimo é {0}")]
        public string login { get; set; }

        [Required(ErrorMessage = "Preencha a senha!")]
        [MaxLength(15, ErrorMessage = "O tamanho máximo é {0}")]
        [MinLength(6, ErrorMessage = "O tamanho mínimo é {0}")] 
        public string senha { get; set; }

        [Required(ErrorMessage = "Preencha a senha!")]
        [MaxLength(150, ErrorMessage = "O tamanho máximo é {0}")]
        [MinLength(6, ErrorMessage = "O tamanho mínimo é {0}")] 
        public string nome { get; set; }

        [ReadOnly(true)]
        public DateTime dataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}