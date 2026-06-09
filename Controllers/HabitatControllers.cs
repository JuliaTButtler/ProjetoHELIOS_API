using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoHELIOS_API.Data;
using ProjetoHELIOS_API.Models;

namespace ProjetoHELIOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class HabitatController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HabitatController(
            AppDbContext context
        )
        {
            _context = context;
        }

        // GET — TODOS
        // api/habitat

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Habitat>>>
            GetHabitats()
        {
            var habitats =
                await _context.Habitats
                    .ToListAsync();

            return Ok(habitats);
        }


        // GET — POR ID
        // api/habitat/1

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Habitat>>
            GetHabitatById(int id)
        {
            var habitat =
                await _context.Habitats
                    .FindAsync(id);

            if (habitat == null)
            {
                return NotFound(
                    "Habitat não encontrado."
                );
            }

            return Ok(habitat);
        }


        // GET — STATUS
        // api/habitat/status/OPERACIONAL

        [HttpGet("status/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Habitat>>>
            GetByStatus(string status)
        {
            var habitats =
                await _context.Habitats
                    .Where(
                        h =>
                        h.StatusOperacional
                        .ToUpper()
                        ==
                        status.ToUpper()
                    )
                    .ToListAsync();

            if (habitats.Count == 0)
            {
                return NotFound(
                    "Nenhum habitat encontrado."
                );
            }

            return Ok(habitats);
        }


        // GET — TIPO
        // api/habitat/tipo/PESQUISA

        [HttpGet("tipo/{tipo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Habitat>>>
            GetByTipo(string tipo)
        {
            var habitats =
                await _context.Habitats
                    .Where(
                        h =>
                        h.TipoHabitat
                        .ToUpper()
                        ==
                        tipo.ToUpper()
                    )
                    .ToListAsync();

            if (habitats.Count == 0)
            {
                return NotFound(
                    "Nenhum habitat encontrado."
                );
            }

            return Ok(habitats);
        }


        // POST
        // api/habitat

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Habitat>>
            Create(
                Habitat habitat
            )
        {
            // Validações

            if (
                string.IsNullOrWhiteSpace(
                    habitat.Nome
                )
            )
            {
                return BadRequest(
                    "Nome obrigatório."
                );
            }

            if (
                string.IsNullOrWhiteSpace(
                    habitat.Localizacao
                )
            )
            {
                return BadRequest(
                    "Localização obrigatória."
                );
            }

            if (
                string.IsNullOrWhiteSpace(
                    habitat.TipoHabitat
                )
            )
            {
                return BadRequest(
                    "Tipo do habitat obrigatório."
                );
            }

            if (
                string.IsNullOrWhiteSpace(
                    habitat.StatusOperacional
                )
            )
            {
                return BadRequest(
                    "Status obrigatório."
                );
            }

            // Validar ID duplicado

            var habitatExistente =
                await _context.Habitats
                    .FirstOrDefaultAsync(
                        h =>
                        h.Id
                        ==
                        habitat.Id
                    );

            if (
                habitatExistente
                !=
                null
            )
            {
                return BadRequest(
                    "Já existe um habitat com esse ID."
                );
            }

            _context.Habitats
                .Add(habitat);

            await _context
                .SaveChangesAsync();

            return CreatedAtAction(
                nameof(
                    GetHabitatById
                ),
                new
                {
                    id =
                    habitat.Id
                },
                habitat
            );
        }


        // PUT
        // api/habitat/1

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult>
            Update(
                int id,
                Habitat habitat
            )
        {
            if (
                id
                !=
                habitat.Id
            )
            {
                return BadRequest(
                    "ID incompatível."
                );
            }

            var existing =
                await _context.Habitats
                    .FindAsync(id);

            if (existing == null)
            {
                return NotFound(
                    "Habitat não encontrado."
                );
            }

            existing.Nome =
                habitat.Nome;

            existing.Localizacao =
                habitat.Localizacao;

            existing.TipoHabitat =
                habitat.TipoHabitat;

            existing.CapacidadeTotal =
                habitat.CapacidadeTotal;

            existing.StatusOperacional =
                habitat.StatusOperacional;

            await _context
                .SaveChangesAsync();

            return NoContent();
        }


        // DELETE
        // api/habitat/1

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>
            Delete(
                int id
            )
        {
            var habitat =
                await _context.Habitats
                    .FindAsync(id);

            if (habitat == null)
            {
                return NotFound(
                    "Habitat não encontrado."
                );
            }

            _context.Habitats
                .Remove(habitat);

            await _context
                .SaveChangesAsync();

            return NoContent();
        }
    }
}