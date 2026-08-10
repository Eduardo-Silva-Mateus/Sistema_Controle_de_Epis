using Controle_de_Epis.ViewModel;
using Microsoft.AspNetCore.Identity;

public interface IUsuarioService
{
    Task<List<UsuarioListViewModel>> GetAllUsuariosAsync();
    Task<UsuarioEditViewModel> GetUsuarioByIdAsync(string id);
    Task <IdentityResult> CreateUsuarioAsync(UsuarioCreateViewModel usuario);
    Task <IdentityResult> UpdateUsuarioAsync(UsuarioEditViewModel usuario);
    Task <IdentityResult> ResetPasswordAsync(string id, string newPassword);
    Task<bool> AlterarStatusAsync(string id, bool novoStatus);
    Task InativarUsuarioAsync(string id);
}