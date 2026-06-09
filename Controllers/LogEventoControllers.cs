using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoHELIOS_API.Data;
using ProjetoHELIOS_API.Models;

namespace ProjetoHELIOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LogEventoController
        : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogEventoController(
            AppDbContext context
        )
        {
            _context = context;
        }

        // GET TODOS
        // api/logevento

        [HttpGet]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        public async Task<
            ActionResult<
                IEnumerable<LogEvento>
            >
        >
        GetLogs()
        {
            var logs =
                await _context
                    .LogsEvento
                    .OrderByDescending(
                        l =>
                        l.DataHoraEvento
                    )
                    .ToListAsync();

            return Ok(logs);
        }



        // GET POR ID
        // api/logevento/1

        [HttpGet("{id}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<
                LogEvento
            >
        >
        GetLogById(
            int id
        )
        {
            var log =
                await _context
                    .LogsEvento
                    .FindAsync(id);

            if (
                log
                ==
                null
            )
            {
                return NotFound(
                    "Log não encontrado."
                );
            }

            return Ok(log);
        }



        // GET POR TIPO
        // api/logevento/tipo/ALERTA

        [HttpGet(
            "tipo/{tipo}"
        )]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<
                IEnumerable<
                    LogEvento
                >
            >
        >
        GetByTipo(
            string tipo
        )
        {
            var logs =
                await _context
                    .LogsEvento
                    .Where(
                        l =>
                        l.TipoEvento
                        .ToUpper()
                        ==
                        tipo
                        .ToUpper()
                    )
                    .OrderByDescending(
                        l =>
                        l.DataHoraEvento
                    )
                    .ToListAsync();

            if (
                logs.Count == 0
            )
            {
                return NotFound(
                    "Nenhum log encontrado."
                );
            }

            return Ok(logs);
        }



        // GET POR NIVEL
        // api/logevento/nivel/CRITICO

        [HttpGet(
            "nivel/{nivel}"
        )]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<
                IEnumerable<
                    LogEvento
                >
            >
        >
        GetByNivel(
            string nivel
        )
        {
            var logs =
                await _context
                    .LogsEvento
                    .Where(
                        l =>
                        l.NivelEvento != null
                        &&
                        l.NivelEvento
                            .ToUpper()
                        ==
                        nivel
                            .ToUpper()
                    )
                    .OrderByDescending(
                        l =>
                        l.DataHoraEvento
                    )
                    .ToListAsync();

            if (
                logs.Count == 0
            )
            {
                return NotFound(
                    "Nenhum log encontrado."
                );
            }

            return Ok(logs);
        }



        // POST
        // api/logevento

        [HttpPost]

        [ProducesResponseType(
            StatusCodes.Status201Created
        )]

        [ProducesResponseType(
            StatusCodes.Status400BadRequest
        )]

        public async Task<
            ActionResult<
                LogEvento
            >
        >
        Create(
            LogEvento log
        )
        {
            if (
                string
                .IsNullOrWhiteSpace(
                    log.TipoEvento
                )
            )
            {
                return BadRequest(
                    "Tipo do evento obrigatório."
                );
            }

            if (
                string
                .IsNullOrWhiteSpace(
                    log.Descricao
                )
            )
            {
                return BadRequest(
                    "Descrição obrigatória."
                );
            }

            _context
                .LogsEvento
                .Add(log);

            await _context
                .SaveChangesAsync();

            return CreatedAtAction(
                nameof(
                    GetLogById
                ),
                new
                {
                    id =
                    log.Id
                },
                log
            );
        }



        // PUT
        // api/logevento/1

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
            LogEvento log
        )
        {
            if (
                id
                !=
                log.Id
            )
            {
                return BadRequest(
                    "ID incompatível."
                );
            }

            var existing =
                await _context
                    .LogsEvento
                    .FindAsync(id);

            if (
                existing
                ==
                null
            )
            {
                return NotFound(
                    "Log não encontrado."
                );
            }

            existing.TipoEvento =
                log.TipoEvento;

            existing.Descricao =
                log.Descricao;

            existing.OrigemEvento =
                log.OrigemEvento;

            existing.NivelEvento =
                log.NivelEvento;

            existing.DataHoraEvento =
                log.DataHoraEvento;

            await _context
                .SaveChangesAsync();

            return NoContent();
        }



        // DELETE
        // api/logevento/1

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
            var log =
                await _context
                    .LogsEvento
                    .FindAsync(id);

            if (
                log
                ==
                null
            )
            {
                return NotFound(
                    "Log não encontrado."
                );
            }

            _context
                .LogsEvento
                .Remove(log);

            await _context
                .SaveChangesAsync();

            return NoContent();
        }
    }
}