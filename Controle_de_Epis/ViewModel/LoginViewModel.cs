using System.ComponentModel.DataAnnotations;

namespace Controle_de_Epis.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public bool LembrarMe { get; set; }
         
    }
}
