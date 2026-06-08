using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoHELIOS_API.Data;
using ProjetoHELIOS_API.Models;

namespace ProjetoHELIOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RegraAlertaController
        : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegraAlertaController(
            AppDbContext context
        )
        {
            _context = context;
        }

        // ==========================
        // GET TODOS
        // api/regraalerta
        // ==========================

        [HttpGet]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        public async Task<
            ActionResult<
                IEnumerable<RegraAlerta>
            >
        >
        GetRegras()
        {
            var regras =
                await _context
                    .RegrasAlerta
                    .ToListAsync();

            return Ok(regras);
        }



        // ==========================
        // GET POR ID
        // api/regraalerta/1
        // ==========================

        [HttpGet("{id}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<
                RegraAlerta
            >
        >
        GetRegraById(
            int id
        )
        {
            var regra =
                await _context
                    .RegrasAlerta
                    .FindAsync(id);

            if (
                regra
                ==
                null
            )
            {
                return NotFound(
                    "Regra não encontrada."
                );
            }

            return Ok(regra);
        }



        // ==========================
        // GET POR SENSOR
        // api/regraalerta/sensor/TEMPERATURA
        // ==========================

        [HttpGet(
            "sensor/{tipoSensor}"
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
                    RegraAlerta
                >
            >
        >
        GetByTipoSensor(
            string tipoSensor
        )
        {
            var regras =
                await _context
                    .RegrasAlerta
                    .Where(
                        r =>
                        r.TipoSensor
                        .ToUpper()
                        ==
                        tipoSensor
                        .ToUpper()
                    )
                    .ToListAsync();

            if (
                regras.Count == 0
            )
            {
                return NotFound(
                    "Nenhuma regra encontrada."
                );
            }

            return Ok(regras);
        }



        // ==========================
        // GET POR STATUS
        // api/regraalerta/ativo/S
        // ==========================

        [HttpGet(
            "ativo/{ativo}"
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
                    RegraAlerta
                >
            >
        >
        GetByStatus(
            string ativo
        )
        {
            var regras =
                await _context
                    .RegrasAlerta
                    .Where(
                        r =>
                        r.Ativo
                        .ToUpper()
                        ==
                        ativo
                        .ToUpper()
                    )
                    .ToListAsync();

            if (
                regras.Count == 0
            )
            {
                return NotFound(
                    "Nenhuma regra encontrada."
                );
            }

            return Ok(regras);
        }



        // ==========================
        // POST
        // api/regraalerta
        // ==========================

        [HttpPost]

        [ProducesResponseType(
            StatusCodes.Status201Created
        )]

        [ProducesResponseType(
            StatusCodes.Status400BadRequest
        )]

        public async Task<
            ActionResult<
                RegraAlerta
            >
        >
        Create(
            RegraAlerta regra
        )
        {
            if (
                string
                .IsNullOrWhiteSpace(
                    regra.TipoSensor
                )
            )
            {
                return BadRequest(
                    "Tipo do sensor obrigatório."
                );
            }

            if (
                string
                .IsNullOrWhiteSpace(
                    regra.NivelCriticidade
                )
            )
            {
                return BadRequest(
                    "Criticidade obrigatória."
                );
            }

            if (
                string
                .IsNullOrWhiteSpace(
                    regra.MensagemPadrao
                )
            )
            {
                return BadRequest(
                    "Mensagem obrigatória."
                );
            }

            if (
                regra.PesoRisco
                <
                0
            )
            {
                return BadRequest(
                    "Peso de risco inválido."
                );
            }

            if (
                regra.ValorMinimo
                !=
                null
                &&
                regra.ValorMaximo
                !=
                null
                &&
                regra.ValorMinimo
                >
                regra.ValorMaximo
            )
            {
                return BadRequest(
                    "Valor mínimo não pode ser maior que o máximo."
                );
            }

            if (
                string
                .IsNullOrWhiteSpace(
                    regra.Ativo
                )
            )
            {
                regra.Ativo = "S";
            }

            _context
                .RegrasAlerta
                .Add(regra);

            await _context
                .SaveChangesAsync();

            return CreatedAtAction(
                nameof(
                    GetRegraById
                ),
                new
                {
                    id =
                    regra.Id
                },
                regra
            );
        }



        // ==========================
        // PUT
        // api/regraalerta/1
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
            RegraAlerta regra
        )
        {
            if (
                id
                !=
                regra.Id
            )
            {
                return BadRequest(
                    "ID incompatível."
                );
            }

            var existing =
                await _context
                    .RegrasAlerta
                    .FindAsync(id);

            if (
                existing
                ==
                null
            )
            {
                return NotFound(
                    "Regra não encontrada."
                );
            }

            if (
                regra.ValorMinimo
                !=
                null
                &&
                regra.ValorMaximo
                !=
                null
                &&
                regra.ValorMinimo
                >
                regra.ValorMaximo
            )
            {
                return BadRequest(
                    "Valor mínimo não pode ser maior que o máximo."
                );
            }

            existing.TipoSensor =
                regra.TipoSensor;

            existing.ValorMinimo =
                regra.ValorMinimo;

            existing.ValorMaximo =
                regra.ValorMaximo;

            existing.NivelCriticidade =
                regra.NivelCriticidade;

            existing.PesoRisco =
                regra.PesoRisco;

            existing.MensagemPadrao =
                regra.MensagemPadrao;

            existing.Ativo =
                regra.Ativo;

            await _context
                .SaveChangesAsync();

            return NoContent();
        }



        // ==========================
        // DELETE
        // api/regraalerta/1
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
            var regra =
                await _context
                    .RegrasAlerta
                    .FindAsync(id);

            if (
                regra
                ==
                null
            )
            {
                return NotFound(
                    "Regra não encontrada."
                );
            }

            _context
                .RegrasAlerta
                .Remove(regra);

            await _context
                .SaveChangesAsync();

            return NoContent();
        }
    }
}