using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiPrimerWebApiM3.DataContext;
using MiPrimerWebApiM3.Entities;

namespace MiPrimerWebApiM3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly AplicationDbContext context;

        public LibrosController(AplicationDbContext context)
        {
            this.context = context;
        }
        // GET: api/Libros
        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            return context.Libros.Include(x=>x.Autor).ToList();
        }

        // GET: api/Libros/5
        [HttpGet("{id}", Name = "ObtenerLibro")]
        public ActionResult<Libro> Get(int id)
        {
            var libro = context.Libros.Include(x=>x.Autor).FirstOrDefault(x => x.Id == id);
            if (libro == null)
            {
                return NotFound();
            }
            return libro;
        }

        // POST: api/Libros
        [HttpPost]
        public ActionResult post([FromBody] Libro libro)
        {
            context.Libros.Add(libro);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro", new { id = libro.Id }, libro);
        }

        // PUT: api/Libros/5
        [HttpPut("{id}")]
        public ActionResult put(int id, [FromBody] Libro value)
        {
            if (id != value.Id) return BadRequest();
            context.Entry(value).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult<Libro> delete(int id)
        {
            var libro = context.Libros.Include(x => x.Autor).FirstOrDefault(x => x.Id == id);
            if (libro == null)
            {
                return NotFound();
            }
            context.Libros.Remove(libro);
            context.SaveChanges();
            return libro;
        }
    }
}
