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
            var usuarios = _userManager.Users.ToList();
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

        public Task<UsuarioEditViewModel> GetUsuarioByIdAsync(string id)
        {
            throw new NotImplementedException();
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

        public Task<IdentityResult> UpdateUsuarioAsync(UsuarioEditViewModel usuario)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> ResetPasswordAsync(string id, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AlterarStatusAsync(string id, bool novoStatus)
        {
            throw new NotImplementedException();
        }

        public Task InativarUsuarioAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
   

 }