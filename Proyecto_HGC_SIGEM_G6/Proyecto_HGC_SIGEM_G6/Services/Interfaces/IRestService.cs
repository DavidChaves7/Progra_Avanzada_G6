using ModelHelper.Models;

namespace Proyecto_HGC_SIGEM_G6.Services.Interfaces
{
    public interface IRestService
    {
        #region Login

        Task<BaseResponse<Usuario>> VerificarCredenciales(string pCorreo, string pContraseña);
        Task<BaseResponse> NuevoUsuario(Usuario pUser);

        #endregion
    }
}
