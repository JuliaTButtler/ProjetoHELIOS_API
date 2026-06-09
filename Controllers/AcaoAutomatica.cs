using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoHELIOS_API.Data;
using ProjetoHELIOS_API.Models;

namespace ProjetoHELIOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AcaoAutomaticaController
        : ControllerBase
    {
        private readonly AppDbContext _context;

        public AcaoAutomaticaController(
            AppDbContext context
        )
        {
            _context = context;
        }

        // GET TODOS
        // api/acaoautomatica

        [HttpGet]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        public async Task<
            ActionResult<
                IEnumerable<
                    AcaoAutomatica
                >
            >
        >
        GetAcoes()
        {
            var acoes =
                await _context
                    .AcoesAutomaticas
                    .ToListAsync();

            return Ok(acoes);
        }



        // GET POR ID
        // api/acaoautomatica/1

        [HttpGet("{id}")]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        [ProducesResponseType(
            StatusCodes.Status404NotFound
        )]

        public async Task<
            ActionResult<
                AcaoAutomatica
            >
        >
        GetAcaoById(
            int id
        )
        {
            var acao =
                await _context
                    .AcoesAutomaticas
                    .FindAsync(id);

            if (
                acao
                ==
                null
            )
            {
                return NotFound(
                    "Ação não encontrada."
                );
            }

            return Ok(acao);
        }



        // GET POR ALERTA
        // api/acaoautomatica/alerta/1

        [HttpGet(
            "alerta/{alertaId}"
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
                    AcaoAutomatica
                >
            >
        >
        GetByAlerta(
            int alertaId
        )
        {
            var acoes =
                await _context
                    .AcoesAutomaticas
                    .Where(
                        a =>
                        a.AlertaId
                        ==
                        alertaId
                    )
                    .ToListAsync();

            if (
                acoes.Count == 0
            )
            {
                return NotFound(
                    "Nenhuma ação encontrada."
                );
            }

            return Ok(acoes);
        }



        // GET POR STATUS
        // api/acaoautomatica/status/EXECUTADA

        [HttpGet(
            "status/{status}"
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
                    AcaoAutomatica
                >
            >
        >
        GetByStatus(
            string status
        )
        {
            var acoes =
                await _context
                    .AcoesAutomaticas
                    .Where(
                        a =>
                        a.StatusAcao
                        .ToUpper()
                        ==
                        status
                        .ToUpper()
                    )
                    .ToListAsync();

            if (
                acoes.Count == 0
            )
            {
                return NotFound(
                    "Nenhuma ação encontrada."
                );
            }

            return Ok(acoes);
        }



        // POST
        // api/acaoautomatica

        [HttpPost]

        [ProducesResponseType(
            StatusCodes.Status201Created
        )]

        [ProducesResponseType(
            StatusCodes.Status400BadRequest
        )]

        public async Task<
            ActionResult<
                AcaoAutomatica
            >
        >
        Create(
            AcaoAutomatica acao
        )
        {
            var alerta =
                await _context
                    .Alertas
                    .FindAsync(
                        acao.AlertaId
                    );

            if (
                alerta
                ==
                null
            )
            {
                return BadRequest(
                    "Alerta inexistente."
                );
            }

            if (
                string
                .IsNullOrWhiteSpace(
                    acao.Descricao
                )
            )
            {
                return BadRequest(
                    "Descrição obrigatória."
                );
            }

            if (
                string
                .IsNullOrWhiteSpace(
                    acao.StatusAcao
                )
            )
            {
                return BadRequest(
                    "Status obrigatório."
                );
            }

            _context
                .AcoesAutomaticas
                .Add(acao);

            await _context
                .SaveChangesAsync();

            return CreatedAtAction(
                nameof(
                    GetAcaoById
                ),
                new
                {
                    id =
                    acao.Id
                },
                acao
            );
        }



        // PUT
        // api/acaoautomatica/1

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
            AcaoAutomatica acao
        )
        {
            if (
                id
                !=
                acao.Id
            )
            {
                return BadRequest(
                    "ID incompatível."
                );
            }

            var existing =
                await _context
                    .AcoesAutomaticas
                    .FindAsync(id);

            if (
                existing
                ==
                null
            )
            {
                return NotFound(
                    "Ação não encontrada."
                );
            }

            var alerta =
                await _context
                    .Alertas
                    .FindAsync(
                        acao.AlertaId
                    );

            if (
                alerta
                ==
                null
            )
            {
                return BadRequest(
                    "Alerta inexistente."
                );
            }

            existing.AlertaId =
                acao.AlertaId;

            existing.Descricao =
                acao.Descricao;

            existing.StatusAcao =
                acao.StatusAcao;

            existing.DataHoraExecucao =
                acao.DataHoraExecucao;

            await _context
                .SaveChangesAsync();

            return NoContent();
        }



        // DELETE
        // api/acaoautomatica/1

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
            var acao =
                await _context
                    .AcoesAutomaticas
                    .FindAsync(id);

            if (
                acao
                ==
                null
            )
            {
                return NotFound(
                    "Ação não encontrada."
                );
            }

            _context
                .AcoesAutomaticas
                .Remove(acao);

            await _context
                .SaveChangesAsync();

            return NoContent();
        }
    }
}