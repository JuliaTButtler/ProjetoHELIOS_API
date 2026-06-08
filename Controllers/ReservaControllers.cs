using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoHELIOS_API.Data;
using ProjetoHELIOS_API.Models;

namespace ProjetoHELIOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ReservaController
        : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservaController(
            AppDbContext context
        )
        {
            _context = context;
        }

        // ==========================
        // GET TODOS
        // api/reserva
        // ==========================

        [HttpGet]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        public async Task<
            ActionResult<
                IEnumerable<Reserva>
            >
        >
        GetReservas()
        {
            var reservas =
                await _context.Reservas
                    .ToListAsync();

            return Ok(reservas);
        }



        // ==========================
        // GET POR ID
        // api/reserva/1
        // ==========================

        [HttpGet("{id}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<Reserva>
        >
        GetReservaById(int id)
        {
            var reserva =
                await _context.Reservas
                    .FindAsync(id);

            if (reserva == null)
            {
                return NotFound(
                    "Reserva não encontrada."
                );
            }

            return Ok(reserva);
        }



        // ==========================
        // GET POR OCUPANTE
        // api/reserva/ocupante/1
        // ==========================

        [HttpGet("ocupante/{ocupanteId}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<
                IEnumerable<Reserva>
            >
        >
        GetByOcupante(
            int ocupanteId
        )
        {
            var reservas =
                await _context.Reservas
                    .Where(
                        r =>
                        r.OcupanteId
                        ==
                        ocupanteId
                    )
                    .ToListAsync();

            if (
                reservas.Count == 0
            )
            {
                return NotFound(
                    "Nenhuma reserva encontrada."
                );
            }

            return Ok(reservas);
        }



        // ==========================
        // GET POR MODULO
        // api/reserva/modulo/1
        // ==========================

        [HttpGet("modulo/{moduloId}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<
                IEnumerable<Reserva>
            >
        >
        GetByModulo(
            int moduloId
        )
        {
            var reservas =
                await _context.Reservas
                    .Where(
                        r =>
                        r.ModuloId
                        ==
                        moduloId
                    )
                    .ToListAsync();

            if (
                reservas.Count == 0
            )
            {
                return NotFound(
                    "Nenhuma reserva encontrada."
                );
            }

            return Ok(reservas);
        }



        // ==========================
        // POST
        // api/reserva
        // ==========================

        [HttpPost]

        [ProducesResponseType(
            StatusCodes.Status201Created
        )]

        [ProducesResponseType(
            StatusCodes.Status400BadRequest
        )]

        public async Task<
            ActionResult<Reserva>
        >
        Create(
            Reserva reserva
        )
        {
            var ocupante =
                await _context.Ocupantes
                    .FindAsync(
                        reserva.OcupanteId
                    );

            if (ocupante == null)
            {
                return BadRequest(
                    "Ocupante inexistente."
                );
            }

            var modulo =
                await _context.Modulos
                    .FindAsync(
                        reserva.ModuloId
                    );

            if (modulo == null)
            {
                return BadRequest(
                    "Módulo inexistente."
                );
            }

            if (
                string.IsNullOrWhiteSpace(
                    reserva.StatusReserva
                )
            )
            {
                return BadRequest(
                    "Status obrigatório."
                );
            }

            if (
                reserva.DataFim != null
                &&
                reserva.DataFim
                <
                reserva.DataInicio
            )
            {
                return BadRequest(
                    "Data fim não pode ser menor que data início."
                );
            }

            _context.Reservas
                .Add(reserva);

            await _context
                .SaveChangesAsync();

            return CreatedAtAction(
                nameof(
                    GetReservaById
                ),
                new
                {
                    id =
                    reserva.Id
                },
                reserva
            );
        }



        // ==========================
        // PUT
        // api/reserva/1
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
            Reserva reserva
        )
        {
            if (
                id
                !=
                reserva.Id
            )
            {
                return BadRequest(
                    "ID incompatível."
                );
            }

            var existing =
                await _context
                    .Reservas
                    .FindAsync(id);

            if (
                existing
                ==
                null
            )
            {
                return NotFound(
                    "Reserva não encontrada."
                );
            }

            var ocupante =
                await _context.Ocupantes
                    .FindAsync(
                        reserva.OcupanteId
                    );

            if (ocupante == null)
            {
                return BadRequest(
                    "Ocupante inexistente."
                );
            }

            var modulo =
                await _context.Modulos
                    .FindAsync(
                        reserva.ModuloId
                    );

            if (modulo == null)
            {
                return BadRequest(
                    "Módulo inexistente."
                );
            }

            if (
                reserva.DataFim != null
                &&
                reserva.DataFim
                <
                reserva.DataInicio
            )
            {
                return BadRequest(
                    "Data fim inválida."
                );
            }

            existing.OcupanteId =
                reserva.OcupanteId;

            existing.ModuloId =
                reserva.ModuloId;

            existing.DataInicio =
                reserva.DataInicio;

            existing.DataFim =
                reserva.DataFim;

            existing.StatusReserva =
                reserva.StatusReserva;

            await _context
                .SaveChangesAsync();

            return NoContent();
        }



        // ==========================
        // DELETE
        // api/reserva/1
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
            var reserva =
                await _context
                    .Reservas
                    .FindAsync(id);

            if (
                reserva
                ==
                null
            )
            {
                return NotFound(
                    "Reserva não encontrada."
                );
            }

            _context.Reservas
                .Remove(
                    reserva
                );

            await _context
                .SaveChangesAsync();

            return NoContent();
        }
    }
}