using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoHELIOS_API.Data;
using ProjetoHELIOS_API.Models;

namespace ProjetoHELIOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LeituraSensorController
        : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeituraSensorController(
            AppDbContext context
        )
        {
            _context = context;
        }

        // GET TODOS
        // api/leiturasensor

        [HttpGet]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        public async Task<
            ActionResult<
                IEnumerable<LeituraSensor>
            >
        >
        GetLeituras()
        {
            var leituras =
                await _context.Leituras
                    .ToListAsync();

            return Ok(
                leituras
            );
        }



        // GET POR ID
        // api/leiturasensor/1

        [HttpGet("{id}")]

        public async Task<
            ActionResult<
                LeituraSensor
            >
        >
        GetLeituraById(
            int id
        )
        {
            var leitura =
                await _context
                    .Leituras
                    .FindAsync(id);

            if (
                leitura
                ==
                null
            )
            {
                return NotFound(
                    "Leitura não encontrada."
                );
            }

            return Ok(
                leitura
            );
        }



        // GET POR SENSOR
        // api/leiturasensor/sensor/1

        [HttpGet(
            "sensor/{sensorId}"
        )]

        public async Task<
            ActionResult<
                IEnumerable<
                    LeituraSensor
                >
            >
        >
        GetBySensor(
            int sensorId
        )
        {
            var leituras =
                await _context
                    .Leituras
                    .Where(
                        l =>
                        l.SensorId
                        ==
                        sensorId
                    )
                    .OrderByDescending(
                        l =>
                        l.DataHoraLeitura
                    )
                    .ToListAsync();

            if (
                leituras.Count
                ==
                0
            )
            {
                return NotFound(
                    "Nenhuma leitura encontrada."
                );
            }

            return Ok(
                leituras
            );
        }



        // GET POR STATUS
        // api/leiturasensor/status/NORMAL

        [HttpGet(
            "status/{status}"
        )]

        public async Task<
            ActionResult<
                IEnumerable<
                    LeituraSensor
                >
            >
        >
        GetByStatus(
            string status
        )
        {
            var leituras =
                await _context
                    .Leituras
                    .Where(
                        l =>
                        l.StatusLeitura
                            .ToUpper()
                        ==
                        status
                            .ToUpper()
                    )
                    .ToListAsync();

            if (
                leituras.Count
                ==
                0
            )
            {
                return NotFound(
                    "Nenhuma leitura encontrada."
                );
            }

            return Ok(
                leituras
            );
        }



        // POST
        // api/leiturasensor

        [HttpPost]

        public async Task<
            ActionResult<
                LeituraSensor
            >
        >
        Create(
            LeituraSensor leitura
        )
        {
            var sensor =
                await _context
                    .Sensores
                    .FindAsync(
                        leitura.SensorId
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

            if (
                string
                .IsNullOrWhiteSpace(
                    leitura.StatusLeitura
                )
            )
            {
                return BadRequest(
                    "Status obrigatório."
                );
            }

            if (
                sensor.LimiteMinimo
                !=
                null
                &&
                leitura.ValorLeitura
                <
                sensor.LimiteMinimo
            )
            {
                return BadRequest(
                    "Valor abaixo do limite mínimo."
                );
            }

            if (
                sensor.LimiteMaximo
                !=
                null
                &&
                leitura.ValorLeitura
                >
                sensor.LimiteMaximo
            )
            {
                return BadRequest(
                    "Valor acima do limite máximo."
                );
            }

            if (
                leitura
                    .DataHoraLeitura
                >
                DateTime.Now
                    .AddMinutes(
                        1
                    )
            )
            {
                return BadRequest(
                    "Data inválida."
                );
            }

            _context
                .Leituras
                .Add(
                    leitura
                );

            await _context
                .SaveChangesAsync();

            return CreatedAtAction(
                nameof(
                    GetLeituraById
                ),
                new
                {
                    id =
                    leitura.Id
                },
                leitura
            );
        }



        // PUT
        // api/leiturasensor/1

        [HttpPut("{id}")]

        public async Task<
            IActionResult
        >
        Update(
            int id,
            LeituraSensor leitura
        )
        {
            if (
                id
                !=
                leitura.Id
            )
            {
                return BadRequest(
                    "ID incompatível."
                );
            }

            var existing =
                await _context
                    .Leituras
                    .FindAsync(id);

            if (
                existing
                ==
                null
            )
            {
                return NotFound(
                    "Leitura não encontrada."
                );
            }

            existing.SensorId =
                leitura.SensorId;

            existing.ValorLeitura =
                leitura.ValorLeitura;

            existing.DataHoraLeitura =
                leitura.DataHoraLeitura;

            existing.StatusLeitura =
                leitura.StatusLeitura;

            await _context
                .SaveChangesAsync();

            return NoContent();
        }



        // DELETE
        // api/leiturasensor/1

        [HttpDelete("{id}")]

        public async Task<
            IActionResult
        >
        Delete(
            int id
        )
        {
            var leitura =
                await _context
                    .Leituras
                    .FindAsync(id);

            if (
                leitura
                ==
                null
            )
            {
                return NotFound(
                    "Leitura não encontrada."
                );
            }

            _context
                .Leituras
                .Remove(
                    leitura
                );

            await _context
                .SaveChangesAsync();

            return NoContent();
        }
    }
}