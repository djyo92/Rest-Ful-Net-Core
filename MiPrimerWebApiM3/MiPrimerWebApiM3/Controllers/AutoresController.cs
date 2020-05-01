using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiPrimerWebApiM3.DataContext;
using MiPrimerWebApiM3.Entities;
using MiPrimerWebApiM3.Services;

namespace MiPrimerWebApiM3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IClaseB claseB;

        public AutoresController(AplicationDbContext context, IClaseB claseB)
        {
            this.context = context;
            this.claseB = claseB;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            claseB.HacerAlgo();
            return context.Autores.Include(x=>x.Libros).ToList();
        }
        [HttpGet("primer")]
        public ActionResult<Autor> GetPrimerAutor()
        {
            return context.Autores.FirstOrDefault();
        }
        [HttpGet("{id}",Name ="ObtenerAutor")]
        public async Task<ActionResult<Autor>> Get(int id)
        {
            var autor = await context.Autores.Include(x=>x.Libros).FirstOrDefaultAsync(x => x.Id == id);
            if (autor == null) {
                return NotFound();
            }
            return autor;
        }
        [HttpPost]
        public ActionResult Post([FromBody] Autor auttor)
        {
            context.Autores.Add(auttor);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor",new { id = auttor.Id},auttor);
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody] Autor value)
        {
            if (id != value.Id) return BadRequest();
            context.Entry(value).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var autor = context.Autores.FirstOrDefault(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            context.Autores.Remove(autor);
            context.SaveChanges();
            return autor;
        }
    }
}