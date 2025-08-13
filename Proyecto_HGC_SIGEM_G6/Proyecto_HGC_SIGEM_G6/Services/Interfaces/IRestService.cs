using System.Threading.Tasks;
using ModelHelpes.Models;                 
using Proyecto_HGC_SIGEM_G6.Services;     

namespace Proyecto_HGC_SIGEM_G6.Services.Interfaces
{
    public interface IRestService
    {

        #region Login

        Task<ApiResult<Usuario>> VerificarCredenciales(string correo, string contrasenaHash);
        Task<ApiResult<Usuario>> NuevoUsuario(Usuario usuario);

        #endregion
    }
}
