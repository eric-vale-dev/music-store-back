namespace MusicStore.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Api.Data;
using MusicStore.Api.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Un DTO (Data Transfer Object) para recibir solo lo que necesitamos de Angular
    public class RegistroUsuarioDto
    {
        public string IdFirebase { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        
    }

    // Este endpoint está protegido, solo alguien con un token válido de Firebase puede llamarlo
    [Authorize]
    [HttpPost("sincronizar")]
    public async Task<IActionResult> SincronizarUsuario([FromBody] RegistroUsuarioDto dto)
    {
        // 1. Verificamos si el usuario ya existe en Postgres usando el ID de Firebase
        var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == dto.IdFirebase);

        if (usuarioExistente != null)
        {
            return Ok(new { mensaje = "El usuario ya existe en la base de datos", usuario = usuarioExistente });
        }

        // 2. Si es la primera vez (ej. tú, el admin original), le asignamos rol Admin, a los demás Cliente.
        // Asumimos que el RolId 1 es Admin y el 2 es Cliente (tendremos que crearlos en la BD después)
        bool esPrimerUsuario = !await _context.Usuarios.AnyAsync();
        int rolIdAsignado = esPrimerUsuario ? 1 : 2;

        // 3. Creamos el nuevo usuario
        var nuevoUsuario = new Usuario
        {
            Id = dto.IdFirebase,
            Nombre = dto.Nombre,
            Correo = dto.Correo,
            RolId = rolIdAsignado,
            FechaRegistro = DateTime.UtcNow
        };

        // 4. Le creamos su carrito vacío inmediatamente
        var nuevoCarrito = new Carrito
        {
            UsuarioId = nuevoUsuario.Id
        };

        // 5. Guardamos todo en la base de datos en una sola transacción
        _context.Usuarios.Add(nuevoUsuario);
        _context.Carritos.Add(nuevoCarrito);
        
        await _context.SaveChangesAsync();

        return Created("", new { mensaje = "Usuario y carrito sincronizados correctamente", usuario = nuevoUsuario });
    }
}