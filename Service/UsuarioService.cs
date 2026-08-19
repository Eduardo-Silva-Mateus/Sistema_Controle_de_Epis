using Controle_de_Epis.Enums;
using Controle_de_Epis.ViewModel;
using Microsoft.AspNetCore.Identity;
using Controle_de_Epis.Models.Identity;

namespace Controle_de_Epis.Service
{
    public class UsuarioService : IUsuarioService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public  UsuarioService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public  async Task<List<UsuarioListViewModel>> GetAllUsuariosAsync()
         {
            var usuarios = _userManager.Users.
                OrderByDescending(u => u.Ativo)
                .ThenBy(u => u.Nome)
                .ToList();
            var lista = new List<UsuarioListViewModel>();  

            foreach (var usuario in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(usuario);
                lista.Add(new UsuarioListViewModel
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email!,
                    Perfil = roles.Any() ? Enum.Parse<PerfilUserEnum>(roles.First()) : PerfilUserEnum.Operador,
                    Ativo = usuario.Ativo
                });
            }   

            return lista;
        }

        public async Task<UsuarioEditViewModel> GetUsuarioByIdAsync(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            
            if(usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            var roles = await _userManager.GetRolesAsync(usuario);
            return new UsuarioEditViewModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email!,
                Perfil = roles.Any() 
                    ? Enum.Parse<PerfilUserEnum>(roles.First()) 
                    : PerfilUserEnum.Operador,
                Ativo = usuario.Ativo
            };
        }

       public async Task<IdentityResult> CreateUsuarioAsync(UsuarioCreateViewModel usuario)
        {
            var novoUsuario = new ApplicationUser
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                Nome = usuario.Nome,
                Ativo = usuario.Ativo
            };

            var result = await _userManager.CreateAsync(
                novoUsuario,
                usuario.Senha
            );

            if (!result.Succeeded)
            {
                return result;
            }

            var roleExiste = await _roleManager.RoleExistsAsync(usuario.Perfil.ToString());

            if (!roleExiste)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Description = "O perfil informado não existe."
                    });
            }

            var roleResult = await _userManager.AddToRoleAsync(
                novoUsuario,
                usuario.Perfil.ToString()
            );

            if (!roleResult.Succeeded)
            {
                return roleResult;
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateUsuarioAsync(UsuarioEditViewModel usuario)
        {
            var usuarioExistente = await _userManager.FindByIdAsync(usuario.Id);

            if(usuarioExistente == null)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Description = "Usuário não encontrado."
                    });
            }

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.UserName = usuario.Email;
            usuarioExistente.Ativo = usuario.Ativo;

            var roleExiste = await _roleManager.RoleExistsAsync(usuario.Perfil.ToString());

            if (!roleExiste)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Description = "O perfil informado não existe"
                    });
            }

            var result = await _userManager.UpdateAsync(usuarioExistente);

            if (!result.Succeeded) 
            {
                return result;
            }

            return IdentityResult.Success;

        }

        public async Task<IdentityResult> ResetPasswordAsync(string id, string newPassword)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if(usuario == null)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Description = "Usuário não encontrado."
                    });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);

            var result = await _userManager.ResetPasswordAsync(usuario, token, newPassword);

            return result;
        }

        public async Task<bool> AlterarStatusAsync(string id, bool novoStatus)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if(usuario == null)
            {
                return false;
            }

            usuario.Ativo = novoStatus;

            var result = await _userManager.UpdateAsync(usuario);

            return result.Succeeded;
        }

        public async Task<IdentityResult> AlterarRoleAsync(string id, PerfilUserEnum novoPerfil)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Description = "Usuário não encontrado."
                    });
            }

            var novaRole = novoPerfil.ToString();
            var roleExiste = await _roleManager.RoleExistsAsync(novaRole);

            if (!roleExiste)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Description = "O perfil informado não existe."
                    });
            }

            var roleAtual = await _userManager.GetRolesAsync(usuario);

            if (roleAtual.Any())
            {
                var removerResult = await _userManager.RemoveFromRolesAsync(
                    usuario,
                    roleAtual
                    );
                if(!removerResult.Succeeded)
                {
                    return removerResult;
                }
            }

            var adicionarResult = await _userManager.AddToRoleAsync(
                usuario,
                novaRole
            );
            return adicionarResult;
        }

    }
}