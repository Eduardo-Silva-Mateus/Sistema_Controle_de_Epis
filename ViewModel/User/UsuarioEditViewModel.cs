using Controle_de_Epis.Enums;

namespace Controle_de_Epis.ViewModel
{
    public class UsuarioEditViewModel
    {
        public required string Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required PerfilUserEnum Perfil { get; set; }
        public  bool Ativo { get; set; }
    }

}