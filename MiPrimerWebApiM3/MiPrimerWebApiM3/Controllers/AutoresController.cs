using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiPrimerWebApiM3.DataContext;
using MiPrimerWebApiM3.Entities;
using MiPrimerWebApiM3.Helpers;
using MiPrimerWebApiM3.Models;
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
        private readonly IMapper mapper;

        public AutoresController(AplicationDbContext context, IClaseB claseB, ILogger<AutoresController> logger, IMapper mapper)
        {
            this.context = context;
            this.claseB = claseB;
            this.logger = logger;
            this.mapper = mapper;
        }
        [HttpGet]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            //throw new Exception("Ocurrio una excepcion");
            logger.LogInformation("Obteniendo los autores");
            claseB.HacerAlgo();
            return context.Autores.Include(x=>x.Libros).ToList();
        }
        [HttpGet("primer")]
        public ActionResult<AutorDTO> GetPrimerAutor()
        {
            var autor = context.Autores.FirstOrDefault();
            var autorDTO = mapper.Map<AutorDTO>(autor);
            return autorDTO;
        }
        [HttpGet("{id}",Name ="ObtenerAutor")]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {
            var autor = await context.Autores.Include(x=>x.Libros).FirstOrDefaultAsync(x => x.Id == id);
            if (autor == null) {
                logger.LogWarning($"El autor con el id {1} no ha sido encontrado");
                return NotFound();
            }
            var autorDTO = mapper.Map<AutorDTO>(autor);
            return autorDTO;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDTO autorCreacion)
        {
            var autor = mapper.Map<Autor>(autorCreacion);
            context.Autores.Add(autor);
            await context.SaveChangesAsync();
            var autorDTO = mapper.Map<AutorDTO>(autor);
            return new CreatedAtRouteResult("ObtenerAutor",new { id = autor.Id}, autorDTO);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id,[FromBody] JsonPatchDocument<AutorCreacionDTO> patchDocument)
        {
            if (patchDocument == null) return BadRequest();
            var autor = context.Autores.FirstOrDefault(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            var autorDTO = mapper.Map<AutorCreacionDTO>(autor);
            
            patchDocument.ApplyTo(autorDTO, ModelState);
            mapper.Map(autorDTO, autor);
            var isValid = TryValidateModel(autor);
            if(!isValid)
            {
                return BadRequest(ModelState);
            }

            await context.SaveChangesAsync();
            return NoContent();
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
            var autorID = context.Autores.Select(x=>x.Id).FirstOrDefault(x => x == id);
            if (autorID == default(int))
            {
                return NotFound();
            }
            context.Autores.Remove(new Autor() { Id=autorID});
            context.SaveChanges();
            return NoContent();
        }
    }
}