using Microsoft.EntityFrameworkCore;
using ModelHelpes.Models; // << Usuario
using Proyecto_HGC_SIGEM_G6.Context;
using Proyecto_HGC_SIGEM_G6.Services.Interfaces;

namespace Proyecto_HGC_SIGEM_G6.Services
{
    public class RestService : IRestService
    {
        private readonly DBContext _db;
        public RestService(DBContext db) => _db = db;

        #region Login
        public async Task<ApiResult<Usuario>> VerificarCredenciales(string correo, string contrasenaHash)
        {
            var user = await _db.Usuarios.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Correo == correo && u.ContrasenaHash == contrasenaHash);

            if (user == null)
                return new ApiResult<Usuario> { hasErrors = true, errorMessage = "Credenciales inválidas" };

            return new ApiResult<Usuario> { data = user, hasErrors = false };
        }

        public async Task<ApiResult<Usuario>> NuevoUsuario(Usuario usuario)
        {
            var existe = await _db.Usuarios.AnyAsync(u => u.Correo == usuario.Correo);
            if (existe)
                return new ApiResult<Usuario> { hasErrors = true, errorMessage = "El correo ya está registrado" };

            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();

            return new ApiResult<Usuario> { data = usuario, hasErrors = false };
        }

        #endregion
    }
}
