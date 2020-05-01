using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AutoresController> logger;

        public AutoresController(AplicationDbContext context, IClaseB claseB, ILogger<AutoresController> logger)
        {
            this.context = context;
            this.claseB = claseB;
            this.logger = logger;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            logger.LogInformation("Obteniendo los autores");
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
                logger.LogWarning($"El autor con el id {1} no ha sido encontrado");
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