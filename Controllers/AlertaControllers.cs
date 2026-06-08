using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoHELIOS_API.Data;
using ProjetoHELIOS_API.Models;

namespace ProjetoHELIOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AlertaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlertaController(
            AppDbContext context
        )
        {
            _context = context;
        }

        // ==========================
        // GET TODOS
        // api/alerta
        // ==========================

        [HttpGet]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        public async Task<
            ActionResult<
                IEnumerable<Alerta>
            >
        >
        GetAlertas()
        {
            var alertas =
                await _context.Alertas
                    .ToListAsync();

            return Ok(alertas);
        }



        // ==========================
        // GET POR ID
        // api/alerta/1
        // ==========================

        [HttpGet("{id}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<Alerta>
        >
        GetAlertaById(int id)
        {
            var alerta =
                await _context.Alertas
                    .FindAsync(id);

            if (alerta == null)
            {
                return NotFound(
                    "Alerta não encontrado."
                );
            }

            return Ok(alerta);
        }



        // ==========================
        // GET POR SENSOR
        // api/alerta/sensor/1
        // ==========================

        [HttpGet("sensor/{sensorId}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<
                IEnumerable<Alerta>
            >
        >
        GetBySensor(
            int sensorId
        )
        {
            var alertas =
                await _context.Alertas
                    .Where(
                        a =>
                        a.SensorId
                        ==
                        sensorId
                    )
                    .ToListAsync();

            if (
                alertas.Count == 0
            )
            {
                return NotFound(
                    "Nenhum alerta encontrado."
                );
            }

            return Ok(alertas);
        }



        // ==========================
        // GET POR MODULO
        // api/alerta/modulo/1
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
                IEnumerable<Alerta>
            >
        >
        GetByModulo(
            int moduloId
        )
        {
            var alertas =
                await _context.Alertas
                    .Where(
                        a =>
                        a.ModuloId
                        ==
                        moduloId
                    )
                    .ToListAsync();

            if (
                alertas.Count == 0
            )
            {
                return NotFound(
                    "Nenhum alerta encontrado."
                );
            }

            return Ok(alertas);
        }



        // ==========================
        // GET POR CRITICIDADE
        // api/alerta/criticidade/ALTO
        // ==========================

        [HttpGet("criticidade/{nivel}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<
                IEnumerable<Alerta>
            >
        >
        GetByCriticidade(
            string nivel
        )
        {
            var alertas =
                await _context.Alertas
                    .Where(
                        a =>
                        a.NivelCriticidade
                        .ToUpper()
                        ==
                        nivel.ToUpper()
                    )
                    .ToListAsync();

            if (
                alertas.Count == 0
            )
            {
                return NotFound(
                    "Nenhum alerta encontrado."
                );
            }

            return Ok(alertas);
        }



        // ==========================
        // POST
        // api/alerta
        // ==========================

        [HttpPost]

        [ProducesResponseType(
            StatusCodes.Status201Created
        )]

        [ProducesResponseType(
            StatusCodes.Status400BadRequest
        )]

        public async Task<
            ActionResult<Alerta>
        >
        Create(
            Alerta alerta
        )
        {
            var sensor =
                await _context.Sensores
                    .FindAsync(
                        alerta.SensorId
                    );

            if (
                sensor
                ==
                null
            )
            {
                return BadRequest(
                    "Sensor inexistente."
                );
            }

            var modulo =
                await _context.Modulos
                    .FindAsync(
                        alerta.ModuloId
                    );

            if (
                modulo
                ==
                null
            )
            {
                return BadRequest(
                    "Módulo inexistente."
                );
            }

            if (
                string.IsNullOrWhiteSpace(
                    alerta.TipoAlerta
                )
            )
            {
                return BadRequest(
                    "Tipo obrigatório."
                );
            }

            if (
                string.IsNullOrWhiteSpace(
                    alerta.Mensagem
                )
            )
            {
                return BadRequest(
                    "Mensagem obrigatória."
                );
            }

            if (
                string.IsNullOrWhiteSpace(
                    alerta.NivelCriticidade
                )
            )
            {
                return BadRequest(
                    "Criticidade obrigatória."
                );
            }

            _context.Alertas
                .Add(alerta);

            await _context
                .SaveChangesAsync();

            return CreatedAtAction(
                nameof(
                    GetAlertaById
                ),
                new
                {
                    id =
                    alerta.Id
                },
                alerta
            );
        }



        // ==========================
        // PUT
        // api/alerta/1
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
            Alerta alerta
        )
        {
            if (
                id
                !=
                alerta.Id
            )
            {
                return BadRequest(
                    "ID incompatível."
                );
            }

            var existing =
                await _context
                    .Alertas
                    .FindAsync(id);

            if (
                existing
                ==
                null
            )
            {
                return NotFound(
                    "Alerta não encontrado."
                );
            }

            existing.ModuloId =
                alerta.ModuloId;

            existing.SensorId =
                alerta.SensorId;

            existing.TipoAlerta =
                alerta.TipoAlerta;

            existing.Mensagem =
                alerta.Mensagem;

            existing.NivelCriticidade =
                alerta.NivelCriticidade;

            existing.DataHoraResolucao =
                alerta.DataHoraResolucao;

            existing.StatusAlerta =
                alerta.StatusAlerta;

            existing.AcaoCorretiva =
                alerta.AcaoCorretiva;

            await _context
                .SaveChangesAsync();

            return NoContent();
        }



        // ==========================
        // DELETE
        // api/alerta/1
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
            var alerta =
                await _context
                    .Alertas
                    .FindAsync(id);

            if (
                alerta
                ==
                null
            )
            {
                return NotFound(
                    "Alerta não encontrado."
                );
            }

            _context
                .Alertas
                .Remove(alerta);

            await _context
                .SaveChangesAsync();

            return NoContent();
        }
    }
}