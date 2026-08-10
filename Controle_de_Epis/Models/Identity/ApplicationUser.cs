using Microsoft.AspNetCore.Identity;

namespace Controle_de_Epis.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;

    }
}

