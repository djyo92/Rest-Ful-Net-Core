using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiPrimerWebApiM3.DataContext;
using MiPrimerWebApiM3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3.Data
{
    public class AutorRepository
    {
        private readonly ILogger<AutorRepository> logger;
        private readonly AplicationDbContext context;

        public AutorRepository(ILogger<AutorRepository> logger, AplicationDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }
        public async Task<List<AutorDTO>> GetAll()
        {
            //throw new Exception("Ocurrio una excepcion");
            logger.LogInformation("Obteniendo los autores Data Reposit");
            //context.Database.
            throw new NotImplementedException();
        }
    }
}
