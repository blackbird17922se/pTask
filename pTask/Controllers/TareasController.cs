using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pTask.Data;
using pTask.Model;

namespace pTask.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TareasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TareasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/tareas
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TareaItem>>> GetTareas()
        {
            return await _context.TareaItem.ToListAsync();
        }

        // POST: api/tareas
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TareaItem>> PostTarea(TareaItem tareaItem)
        {
            if (tareaItem == null)
            {
                return BadRequest("La tarea no puede ser nula.");
            }

            _context.TareaItem.Add(tareaItem);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, $"Error al guardar la tarea: {ex.Message}");
            }

            return CreatedAtAction("GetTareas", new { id =tareaItem.Id }, tareaItem);
        }

        // PUT: api/tareas/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(int id, TareaItem tareaItem)
        {
            // Si el ID en la URL no coincide con el ID de la tarea, devuelve un error 400 (Bad Request)
            if (id != tareaItem.Id)
            {
                return BadRequest("el id no coincide");
            }

            // Marca la tarea como modificada
            _context.Entry(tareaItem).State = EntityState.Modified;

            try
            {
                // Intenta guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            // Captura excepciones de concurrencia
            catch (DbUpdateConcurrencyException)
            {
                if (!TareaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Si la actualización fue exitosa, devuelve un código 204 (No Content)
            return NoContent();
        }

        // DELETE: api/tareas/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _context.TareaItem.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            _context.TareaItem.Remove(tarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // Mtodo privado para verifucar si una tarea existe
        private bool TareaExists(int id)
        {
            return _context.TareaItem.Any(t => t.Id == id);
        }
    }
}
