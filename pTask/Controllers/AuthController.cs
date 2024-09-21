using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pTask.Data;
using pTask.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace pTask.Controllers
{
    /// <summary>
    /// controlador para gestionar la autenticación, permitiendo que el usuario inicie sesión.*/
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /* La interfaz IConfiguration en ASP.NET Core se utiliza para acceder a la 
         * configuración de la aplicación, como las configuraciones definidas en el 
         * archivo appsettings.json, variables de entorno, líneas de comandos, entre otros.
        */
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Recibe el correo y la contraseña del usuario en un POST. Busca el usuario en la base de datos 
        /// y si es válido, genera un token JWT y lo retorna.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario request)
        {
            // Busca el usuario en la base de datos
            //var usaurio = await _context.Usuarios
            //    .FirstOrDefaultAsync(u => u.Correo == request.Correo && u.Password == request.Password);
            var usaurio = _context.Usuarios.SingleOrDefault(u => u.Nombre == request.Nombre);

            // si el usuario no existe o pass invalido
            if (usaurio == null || !BCrypt.Net.BCrypt.Verify(request.PasswordHash, usaurio.PasswordHash))
            {
                return Unauthorized("Usuario o contraseña incorrectos");
            }

            // Generar el token JWT
            var token = GenerarJwtToken(usaurio);

            return Ok(new { token });

        }

        private string GenerarJwtToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Nombre),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])), //tiempo de expiracion e totem
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
