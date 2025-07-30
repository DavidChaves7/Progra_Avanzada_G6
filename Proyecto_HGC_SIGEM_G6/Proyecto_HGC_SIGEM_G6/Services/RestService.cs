using Microsoft.EntityFrameworkCore;
using ModelHelper.Models;
using Proyecto_HGC_SIGEM_G6.Context;
using Proyecto_HGC_SIGEM_G6.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Proyecto_HGC_SIGEM_G6.Services
{
    public class RestService : IRestService
    {
        private readonly HttpClient _httpClient;
        private readonly DBContext _context;

        public RestService(HttpClient httpClient, DBContext context)
        {
            _context = context;
            _httpClient = httpClient;
        }


        #region Login

        public async Task<BaseResponse<Usuario>> VerificarCredenciales(string pCorreo, string pContraseña)
        {
            var res = new BaseResponse<Usuario>();
            try
            {
                string contraseñaHasheada = Hashear(pContraseña);

                res.data = await _context.Usuario
                    .FirstOrDefaultAsync(u => u.Correo == pCorreo && u.ContraseñaHash == contraseñaHasheada);

                res.hasErrors = false;
            }
            catch (Exception ex)
            {
                res.hasErrors = true;
                res.errorMessage = "Error al verificar credenciales: " + ex.Message;
            }

            return res;
        }

        public async Task<BaseResponse> NuevoUsuario(Usuario pUser)
        {
            var res = new BaseResponse();
            try
            {
                var user = _context.Usuario.Where(u => u.Correo == pUser.Correo).FirstOrDefault();
                if (user != null)
                {
                    res.hasErrors = true;
                    res.errorMessage = "Ya existe un usuario con ese correo.";
                    return res;
                }

                pUser.ContraseñaHash = Hashear(pUser.ContraseñaHash);
                await _context.Usuario.AddAsync(pUser);
                await _context.SaveChangesAsync();

                res.hasErrors = false;
            }
            catch (Exception ex)
            {
                res.hasErrors = true;
                res.errorMessage = "Error al crear usuario: " + ex.Message;
            }

            return res;
        }


        private string Hashear(string pContraseña)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(pContraseña);
                var hashBytes = sha256.ComputeHash(bytes);
                var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                return hash;
            }
        }

        #endregion
    }
}
