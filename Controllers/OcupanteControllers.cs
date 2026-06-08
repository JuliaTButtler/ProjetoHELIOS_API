using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoHELIOS_API.Data;
using ProjetoHELIOS_API.Models;

namespace ProjetoHELIOS_API.Controllers
{
    [ApiController]

    [Route("api/[controller]")]

    [Produces("application/json")]

    public class OcupanteController
        : ControllerBase
    {
        private readonly AppDbContext _context;

        public OcupanteController(
            AppDbContext context
        )
        {
            _context = context;
        }

        // ==========================
        // GET TODOS
        // api/ocupante
        // ==========================

        [HttpGet]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        public async Task<
            ActionResult<
                IEnumerable<Ocupante>
            >
        >
        GetOcupantes()
        {
            var ocupantes =
                await _context
                    .Ocupantes
                    .ToListAsync();

            return Ok(
                ocupantes
            );
        }



        // ==========================
        // GET POR ID
        // api/ocupante/1
        // ==========================

        [HttpGet("{id}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<Ocupante>
        >
        GetOcupanteById(
            int id
        )
        {
            var ocupante =
                await _context
                    .Ocupantes
                    .FindAsync(id);

            if (
                ocupante
                ==
                null
            )
            {
                return NotFound(
                    "Ocupante não encontrado."
                );
            }

            return Ok(
                ocupante
            );
        }



        // ==========================
        // GET POR STATUS
        // api/ocupante/status/ATIVO
        // ==========================

        [HttpGet("status/{status}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<
                IEnumerable<Ocupante>
            >
        >
        GetByStatus(
            string status
        )
        {
            var ocupantes =
                await _context
                    .Ocupantes
                    .Where(
                        o =>
                        o.StatusOcupante
                        .ToUpper()
                        ==
                        status
                        .ToUpper()
                    )
                    .ToListAsync();

            if (
                ocupantes.Count
                ==
                0
            )
            {
                return NotFound(
                    "Nenhum ocupante encontrado."
                );
            }

            return Ok(
                ocupantes
            );
        }



        // ==========================
        // GET POR FUNÇÃO
        // api/ocupante/funcao/ENGENHEIRO
        // ==========================

        [HttpGet("funcao/{funcao}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<
                IEnumerable<Ocupante>
            >
        >
        GetByFuncao(
            string funcao
        )
        {
            var ocupantes =
                await _context
                    .Ocupantes
                    .Where(
                        o =>
                        o.Funcao != null
                        &&
                        o.Funcao
                        .ToUpper()
                        ==
                        funcao
                        .ToUpper()
                    )
                    .ToListAsync();

            if (
                ocupantes.Count
                ==
                0
            )
            {
                return NotFound(
                    "Nenhum ocupante encontrado."
                );
            }

            return Ok(
                ocupantes
            );
        }



        // ==========================
        // POST
        // api/ocupante
        // ==========================

        [HttpPost]

        [ProducesResponseType(
            StatusCodes.Status201Created
        )]

        [ProducesResponseType(
            StatusCodes.Status400BadRequest
        )]

        public async Task<
            ActionResult<Ocupante>
        >
        Create(
            Ocupante ocupante
        )
        {
            if (
                string.IsNullOrWhiteSpace(
                    ocupante.Nome
                )
            )
            {
                return BadRequest(
                    "Nome obrigatório."
                );
            }

            if (
                string.IsNullOrWhiteSpace(
                    ocupante.StatusOcupante
                )
            )
            {
                return BadRequest(
                    "Status obrigatório."
                );
            }

            var idExiste =
                await _context
                    .Ocupantes
                    .CountAsync(
                        o =>
                        o.Id
                        ==
                        ocupante.Id
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

            _context
                .Ocupantes
                .Add(
                    ocupante
                );

            await _context
                .SaveChangesAsync();

            return CreatedAtAction(
                nameof(
                    GetOcupanteById
                ),
                new
                {
                    id =
                    ocupante.Id
                },
                ocupante
            );
        }



        // ==========================
        // PUT
        // api/ocupante/1
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
            Ocupante ocupante
        )
        {
            if (
                id
                !=
                ocupante.Id
            )
            {
                return BadRequest(
                    "ID incompatível."
                );
            }

            var existing =
                await _context
                    .Ocupantes
                    .FindAsync(id);

            if (
                existing
                ==
                null
            )
            {
                return NotFound(
                    "Ocupante não encontrado."
                );
            }

            existing.Nome =
                ocupante.Nome;

            existing.Funcao =
                ocupante.Funcao;

            existing.StatusOcupante =
                ocupante.StatusOcupante;

            await _context
                .SaveChangesAsync();

            return NoContent();
        }



        // ==========================
        // DELETE
        // api/ocupante/1
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
            var ocupante =
                await _context
                    .Ocupantes
                    .FindAsync(id);

            if (
                ocupante
                ==
                null
            )
            {
                return NotFound(
                    "Ocupante não encontrado."
                );
            }

            _context
                .Ocupantes
                .Remove(
                    ocupante
                );

            await _context
                .SaveChangesAsync();

            return NoContent();
        }
    }
}