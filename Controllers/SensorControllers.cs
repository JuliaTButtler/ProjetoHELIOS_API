using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoHELIOS_API.Data;
using ProjetoHELIOS_API.Models;

namespace ProjetoHELIOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SensorController
        : ControllerBase
    {
        private readonly AppDbContext _context;

        public SensorController(
            AppDbContext context
        )
        {
            _context = context;
        }

        // GET TODOS
        // api/sensor

        [HttpGet]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        public async Task<
            ActionResult<
                IEnumerable<Sensor>
            >
        >
        GetSensores()
        {
            var sensores =
                await _context.Sensores
                    .ToListAsync();

            return Ok(sensores);
        }



        // GET POR ID
        // api/sensor/1

        [HttpGet("{id}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<Sensor>
        >
        GetSensorById(
            int id
        )
        {
            var sensor =
                await _context.Sensores
                    .FindAsync(id);

            if (
                sensor
                ==
                null
            )
            {
                return NotFound(
                    "Sensor não encontrado."
                );
            }

            return Ok(sensor);
        }



        // GET POR MODULO
        // api/sensor/modulo/1

        [HttpGet("modulo/{moduloId}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<
                IEnumerable<Sensor>
            >
        >
        GetByModulo(
            int moduloId
        )
        {
            var sensores =
                await _context.Sensores
                    .Where(
                        s =>
                        s.ModuloId
                        ==
                        moduloId
                    )
                    .ToListAsync();

            if (
                sensores.Count
                ==
                0
            )
            {
                return NotFound(
                    "Nenhum sensor encontrado."
                );
            }

            return Ok(
                sensores
            );
        }



        // GET POR TIPO
        // api/sensor/tipo/TEMPERATURA

        [HttpGet("tipo/{tipo}")]

        public async Task<
            ActionResult<
                IEnumerable<Sensor>
            >
        >
        GetByTipo(
            string tipo
        )
        {
            var sensores =
                await _context.Sensores
                    .Where(
                        s =>
                        s.TipoSensor
                            .ToUpper()
                        ==
                        tipo
                            .ToUpper()
                    )
                    .ToListAsync();

            if (
                sensores.Count
                ==
                0
            )
            {
                return NotFound(
                    "Nenhum sensor encontrado."
                );
            }

            return Ok(
                sensores
            );
        }



        // POST
        // api/sensor

        [HttpPost]

        [ProducesResponseType(
            StatusCodes.Status201Created
        )]

        [ProducesResponseType(
            StatusCodes.Status400BadRequest
        )]

        public async Task<
            ActionResult<Sensor>
        >
        Create(
            Sensor sensor
        )
        {
            var modulo =
                await _context.Modulos
                    .FindAsync(
                        sensor.ModuloId
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
                string
                .IsNullOrWhiteSpace(
                    sensor.NomeSensor
                )
            )
            {
                return BadRequest(
                    "Nome obrigatório."
                );
            }

            if (
                string
                .IsNullOrWhiteSpace(
                    sensor.TipoSensor
                )
            )
            {
                return BadRequest(
                    "Tipo obrigatório."
                );
            }

            if (
                string
                .IsNullOrWhiteSpace(
                    sensor.StatusSensor
                )
            )
            {
                return BadRequest(
                    "Status obrigatório."
                );
            }

            if (
                string
                .IsNullOrWhiteSpace(
                    sensor.UnidadeMedida
                )
            )
            {
                return BadRequest(
                    "Unidade obrigatória."
                );
            }

            if (
                sensor.LimiteMinimo
                >
                sensor.LimiteMaximo
            )
            {
                return BadRequest(
                    "Limite mínimo maior que máximo."
                );
            }

            if (
                sensor.IntervaloLeituraSegundos
                <=
                0
                &&
                sensor.IntervaloLeituraSegundos
                !=
                null
            )
            {
                return BadRequest(
                    "Intervalo inválido."
                );
            }

            _context
                .Sensores
                .Add(
                    sensor
                );

            await _context
                .SaveChangesAsync();

            return CreatedAtAction(
                nameof(
                    GetSensorById
                ),
                new
                {
                    id =
                    sensor.Id
                },
                sensor
            );
        }



        // PUT
        // api/sensor/1

        [HttpPut("{id}")]

        public async Task<
            IActionResult
        >
        Update(
            int id,
            Sensor sensor
        )
        {
            if (
                id
                !=
                sensor.Id
            )
            {
                return BadRequest(
                    "ID incompatível."
                );
            }

            var existing =
                await _context
                    .Sensores
                    .FindAsync(id);

            if (
                existing
                ==
                null
            )
            {
                return NotFound(
                    "Sensor não encontrado."
                );
            }

            existing.ModuloId =
                sensor.ModuloId;

            existing.NomeSensor =
                sensor.NomeSensor;

            existing.TipoSensor =
                sensor.TipoSensor;

            existing.StatusSensor =
                sensor.StatusSensor;

            existing.UnidadeMedida =
                sensor.UnidadeMedida;

            existing.LimiteMinimo =
                sensor.LimiteMinimo;

            existing.LimiteMaximo =
                sensor.LimiteMaximo;

            existing.IntervaloLeituraSegundos =
                sensor.IntervaloLeituraSegundos;

            existing.DataInstalacao =
                sensor.DataInstalacao;

            await _context
                .SaveChangesAsync();

            return NoContent();
        }



        // DELETE
        // api/sensor/1

        [HttpDelete("{id}")]

        public async Task<
            IActionResult
        >
        Delete(
            int id
        )
        {
            var sensor =
                await _context
                    .Sensores
                    .FindAsync(id);

            if (
                sensor
                ==
                null
            )
            {
                return NotFound(
                    "Sensor não encontrado."
                );
            }

            _context
                .Sensores
                .Remove(
                    sensor
                );

            await _context
                .SaveChangesAsync();

            return NoContent();
        }
    }
}