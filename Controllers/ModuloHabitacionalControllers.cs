using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoHELIOS_API.Data;
using ProjetoHELIOS_API.Models;

namespace ProjetoHELIOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ModuloHabitacionalController
        : ControllerBase
    {
        private readonly AppDbContext _context;

        public ModuloHabitacionalController(
            AppDbContext context
        )
        {
            _context = context;
        }

        // ==========================
        // GET TODOS
        // ==========================

        [HttpGet]

        [ProducesResponseType(
            StatusCodes.Status200OK
        )]

        public async Task<
            ActionResult<
                IEnumerable<ModuloHabitacional>
            >
        >
        GetModulos()
        {
            var modulos =
                await _context.Modulos
                    .ToListAsync();

            return Ok(modulos);
        }


        // ==========================
        // GET POR ID
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
                ModuloHabitacional
            >
        >
        GetModuloById(
            int id
        )
        {
            var modulo =
                await _context.Modulos
                    .FindAsync(id);

            if (
                modulo
                ==
                null
            )
            {
                return NotFound(
                    "Módulo não encontrado."
                );
            }

            return Ok(modulo);
        }


        // ==========================
        // GET POR HABITAT
        // ==========================

        [HttpGet(
            "habitat/{habitatId}"
        )]

        public async Task<
            ActionResult<
                IEnumerable<
                    ModuloHabitacional
                >
            >
        >
        GetByHabitat(
            int habitatId
        )
        {
            var modulos =
                await _context.Modulos
                    .Where(
                        m =>
                        m.HabitatId
                        ==
                        habitatId
                    )
                    .ToListAsync();

            if (
                modulos.Count
                ==
                0
            )
            {
                return NotFound(
                    "Nenhum módulo encontrado."
                );
            }

            return Ok(modulos);
        }


        // ==========================
        // GET POR STATUS
        // ==========================

        [HttpGet(
            "status/{status}"
        )]

        public async Task<
            ActionResult<
                IEnumerable<
                    ModuloHabitacional
                >
            >
        >
        GetByStatus(
            string status
        )
        {
            var modulos =
                await _context.Modulos
                    .Where(
                        m =>
                        m.StatusModulo
                        .ToUpper()
                        ==
                        status.ToUpper()
                    )
                    .ToListAsync();

            if (
                modulos.Count
                ==
                0
            )
            {
                return NotFound(
                    "Nenhum módulo encontrado."
                );
            }

            return Ok(modulos);
        }


        // ==========================
        // POST
        // ==========================

        [HttpPost]

        public async Task<
            ActionResult<
                ModuloHabitacional
            >
        >
        Create(
            ModuloHabitacional modulo
        )
        {
            // ID duplicado

            var existente =
                await _context.Modulos
                    .FirstOrDefaultAsync(
                        m =>
                        m.Id
                        ==
                        modulo.Id
                    );

            if (
                existente
                !=
                null
            )
            {
                return BadRequest(
                    "Já existe um módulo com esse ID."
                );
            }


            // Habitat

            var habitat =
                await _context.Habitats
                    .FindAsync(
                        modulo.HabitatId
                    );

            if (
                habitat
                ==
                null
            )
            {
                return BadRequest(
                    "Habitat inexistente."
                );
            }


            // Campos obrigatórios

            if (
                string.IsNullOrWhiteSpace(
                    modulo.NomeModulo
                )
            )
            {
                return BadRequest(
                    "Nome obrigatório."
                );
            }

            if (
                string.IsNullOrWhiteSpace(
                    modulo.TipoModulo
                )
            )
            {
                return BadRequest(
                    "Tipo obrigatório."
                );
            }

            if (
                string.IsNullOrWhiteSpace(
                    modulo.StatusModulo
                )
            )
            {
                return BadRequest(
                    "Status obrigatório."
                );
            }

            if (
                string.IsNullOrWhiteSpace(
                    modulo.NivelRisco
                )
            )
            {
                return BadRequest(
                    "Nível de risco obrigatório."
                );
            }


            // Regras

            if (
                modulo.CapacidadeOcupantes
                <
                0
            )
            {
                return BadRequest(
                    "Capacidade inválida."
                );
            }

            if (
                modulo.OcupacaoAtual
                >
                modulo.CapacidadeOcupantes
            )
            {
                return BadRequest(
                    "Ocupação maior que capacidade."
                );
            }

            _context.Modulos
                .Add(
                    modulo
                );

            await _context
                .SaveChangesAsync();

            return CreatedAtAction(
                nameof(
                    GetModuloById
                ),
                new
                {
                    id =
                    modulo.Id
                },
                modulo
            );
        }


        // ==========================
        // PUT
        // ==========================

        [HttpPut("{id}")]

        public async Task<
            IActionResult
        >
        Update(
            int id,
            ModuloHabitacional modulo
        )
        {
            if (
                id
                !=
                modulo.Id
            )
            {
                return BadRequest(
                    "ID incompatível."
                );
            }

            var existing =
                await _context
                    .Modulos
                    .FindAsync(
                        id
                    );

            if (
                existing
                ==
                null
            )
            {
                return NotFound(
                    "Módulo não encontrado."
                );
            }


            var habitat =
                await _context
                    .Habitats
                    .FindAsync(
                        modulo.HabitatId
                    );

            if (
                habitat
                ==
                null
            )
            {
                return BadRequest(
                    "Habitat inexistente."
                );
            }

            if (
                modulo.OcupacaoAtual
                >
                modulo.CapacidadeOcupantes
            )
            {
                return BadRequest(
                    "Ocupação maior que capacidade."
                );
            }


            existing.HabitatId =
                modulo.HabitatId;

            existing.NomeModulo =
                modulo.NomeModulo;

            existing.TipoModulo =
                modulo.TipoModulo;

            existing.CapacidadeOcupantes =
                modulo.CapacidadeOcupantes;

            existing.OcupacaoAtual =
                modulo.OcupacaoAtual;

            existing.StatusModulo =
                modulo.StatusModulo;

            existing.NivelRisco =
                modulo.NivelRisco;

            existing.IndiceRisco =
                modulo.IndiceRisco;

            await _context
                .SaveChangesAsync();

            return NoContent();
        }


        // ==========================
        // DELETE
        // ==========================

        [HttpDelete("{id}")]

        public async Task<
            IActionResult
        >
        Delete(
            int id
        )
        {
            var modulo =
                await _context
                    .Modulos
                    .FindAsync(
                        id
                    );

            if (
                modulo
                ==
                null
            )
            {
                return NotFound(
                    "Módulo não encontrado."
                );
            }

            _context.Modulos
                .Remove(
                    modulo
                );

            await _context
                .SaveChangesAsync();

            return NoContent();
        }
    }
}