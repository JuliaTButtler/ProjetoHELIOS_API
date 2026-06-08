using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoHELIOS_API.Data;
using ProjetoHELIOS_API.Models;

namespace ProjetoHELIOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]

    public class UsuarioController
        : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(
            AppDbContext context
        )
        {
            _context = context;
        }

        // ==========================
        // GET TODOS
        // api/usuario
        // ==========================

        [HttpGet]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        public async Task<
            ActionResult<
                IEnumerable<Usuario>
            >
        >
        GetUsuarios()
        {
            var usuarios =
                await _context.Usuarios
                    .ToListAsync();

            return Ok(usuarios);
        }



        // ==========================
        // GET POR ID
        // api/usuario/1
        // ==========================

        [HttpGet("{id}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<Usuario>
        >
        GetUsuarioById(
            int id
        )
        {
            var usuario =
                await _context
                    .Usuarios
                    .FindAsync(id);

            if (
                usuario
                ==
                null
            )
            {
                return NotFound(
                    "Usuário não encontrado."
                );
            }

            return Ok(usuario);
        }



        // ==========================
        // GET POR EMAIL
        // api/usuario/email/x
        // ==========================

        [HttpGet("email/{email}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<Usuario>
        >
        GetByEmail(
            string email
        )
        {
            var usuario =
                await _context
                    .Usuarios
                    .FirstOrDefaultAsync(
                        u =>
                        u.Email
                        .ToUpper()
                        ==
                        email
                        .ToUpper()
                    );

            if (
                usuario
                ==
                null
            )
            {
                return NotFound(
                    "Usuário não encontrado."
                );
            }

            return Ok(usuario);
        }



        // ==========================
        // GET POR TIPO
        // api/usuario/tipo/ADMIN
        // ==========================

        [HttpGet("tipo/{tipo}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<
                IEnumerable<Usuario>
            >
        >
        GetByTipo(
            string tipo
        )
        {
            var usuarios =
                await _context
                    .Usuarios
                    .Where(
                        u =>
                        u.TipoUsuario
                        .ToUpper()
                        ==
                        tipo
                        .ToUpper()
                    )
                    .ToListAsync();

            if (
                usuarios.Count
                ==
                0
            )
            {
                return NotFound(
                    "Nenhum usuário encontrado."
                );
            }

            return Ok(
                usuarios
            );
        }



        // ==========================
        // POST
        // api/usuario
        // ==========================

        [HttpPost]

        [ProducesResponseType(
            StatusCodes.Status201Created
        )]

        [ProducesResponseType(
            StatusCodes.Status400BadRequest
        )]

        public async Task<
            ActionResult<Usuario>
        >
        Create(
            Usuario usuario
        )
        {
            if (
                string.IsNullOrWhiteSpace(
                    usuario.Nome
                )
            )
            {
                return BadRequest(
                    "Nome obrigatório."
                );
            }

            if (
                string.IsNullOrWhiteSpace(
                    usuario.Email
                )
            )
            {
                return BadRequest(
                    "Email obrigatório."
                );
            }

            if (
                string.IsNullOrWhiteSpace(
                    usuario.Senha
                )
            )
            {
                return BadRequest(
                    "Senha obrigatória."
                );
            }

            var idExiste =
                await _context
                    .Usuarios
                    .CountAsync(
                        u =>
                        u.Id
                        ==
                        usuario.Id
                    );

            if (
                idExiste
                >
                0
            )
            {
                return BadRequest(
                    "ID já existe."
                );
            }

            var emailExiste =
                await _context
                    .Usuarios
                    .CountAsync(
                        u =>
                        u.Email
                        .ToUpper()
                        ==
                        usuario.Email
                        .ToUpper()
                    );

            if (
                emailExiste
                >
                0
            )
            {
                return BadRequest(
                    "Email já cadastrado."
                );
            }

            _context
                .Usuarios
                .Add(
                    usuario
                );

            await _context
                .SaveChangesAsync();

            return CreatedAtAction(
                nameof(
                    GetUsuarioById
                ),
                new
                {
                    id =
                    usuario.Id
                },
                usuario
            );
        }



        // ==========================
        // PUT
        // api/usuario/1
        // ==========================

        [HttpPut("{id}")]

        [ProducesResponseType(
            StatusCodes.Status204NoContent
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        [ProducesResponseType(
            StatusCodes.Status400BadRequest
        )]

        public async Task<
            IActionResult
        >
        Update(
            int id,
            Usuario usuario
        )
        {
            if (
                id
                !=
                usuario.Id
            )
            {
                return BadRequest(
                    "ID incompatível."
                );
            }

            var existing =
                await _context
                    .Usuarios
                    .FindAsync(id);

            if (
                existing
                ==
                null
            )
            {
                return NotFound(
                    "Usuário não encontrado."
                );
            }

            existing.Nome =
                usuario.Nome;

            existing.Email =
                usuario.Email;

            existing.Senha =
                usuario.Senha;

            existing.TipoUsuario =
                usuario.TipoUsuario;

            existing.StatusUsuario =
                usuario.StatusUsuario;

            existing.NivelAcesso =
                usuario.NivelAcesso;

            await _context
                .SaveChangesAsync();

            return NoContent();
        }



        // ==========================
        // DELETE
        // api/usuario/1
        // ==========================

        [HttpDelete("{id}")]

        [ProducesResponseType(
            StatusCodes.Status204NoContent
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            IActionResult
        >
        Delete(
            int id
        )
        {
            var usuario =
                await _context
                    .Usuarios
                    .FindAsync(id);

            if (
                usuario
                ==
                null
            )
            {
                return NotFound(
                    "Usuário não encontrado."
                );
            }

            _context
                .Usuarios
                .Remove(
                    usuario
                );

            await _context
                .SaveChangesAsync();

            return NoContent();
        }
    }
}