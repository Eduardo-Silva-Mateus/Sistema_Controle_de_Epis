using System.ComponentModel.DataAnnotations;
using Controle_de_Epis.Enums;

namespace Controle_de_Epis.ViewModel
{
    public class UsuarioCreateViewModel
    {
        public required string Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
         public required string Nome { get; set; }

         [Required(ErrorMessage = "Email é obrigatório")]
         [EmailAddress(ErrorMessage = "Informe um email válido")]
         public required string Email { get; set; }

         [Required(ErrorMessage = "Senha é obrigatória")]
         [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres")]
         [DataType(DataType.Password)]
        public required string Senha { get; set; }

        [Required(ErrorMessage = "Confirmação de senha é obrigatória")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem")]
         public required string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "Perfil é obrigatório")]
         public required PerfilUserEnum Perfil { get; set; }
         public bool Ativo { get; set; } = true;
    }

}