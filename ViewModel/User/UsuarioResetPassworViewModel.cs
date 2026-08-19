using System.ComponentModel.DataAnnotations;

namespace Controle_de_Epis.ViewModel
{
    public class UsuarioResetPasswordViewModel
    {
        public required string Id { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
         [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres")]
        [DataType(DataType.Password)]
        public required string Senha { get; set; }

        [Required(ErrorMessage = "Confirmação de senha é obrigatória")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem")]
        public required string ConfirmarSenha { get; set; }
    }

}