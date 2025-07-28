public class Usuario
{
    public int IdUsuario { get; set; }

    [Required]
    public string Nombre { get; set; }

    [Required, EmailAddress]
    public string Correo { get; set; }

    [Required]
    public string ContraseñaHash { get; set; }

    [Required]
    public string Rol { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public bool Activo { get; set; } = true;
}
