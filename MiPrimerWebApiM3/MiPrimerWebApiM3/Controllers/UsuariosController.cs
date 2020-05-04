using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiPrimerWebApiM3.DataContext;
using MiPrimerWebApiM3.Entities;

namespace MiPrimerWebApiM3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly UserManager<AplicationUser> userManager;

        public UsuariosController(AplicationDbContext context,
            UserManager<AplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        [Route("CrearRol")]
        public async Task<ActionResult> CrearRol() {
            var role = "Manager";

            var newRole = new IdentityRole()
            {
                Name = role,
                NormalizedName = role.ToUpper() // Mandatory.
            };

            //! Adding the new Role into database and saving
            await context.Roles.AddAsync(newRole);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("AsignarUsuarioRol")]
        public async Task<ActionResult> AsignarRolUsuario(EditarRolDTO editarRolDTO)
        {
            var usuario = await userManager.FindByIdAsync(editarRolDTO.UserId);
            if (usuario == null) { return NotFound($"Usuario {editarRolDTO.UserId} no encontrado"); }
            await userManager.AddClaimAsync(usuario, new Claim(ClaimTypes.Role, editarRolDTO.RoleName));
            await userManager.AddToRoleAsync(usuario, editarRolDTO.RoleName);
            return Ok();
        }
        [HttpPost]
        [Route("RemoverUsuarioRol")]
        public async Task<ActionResult> RemoverUsuarioRol(EditarRolDTO editarRolDTO)
        {
            var usuario = await userManager.FindByIdAsync(editarRolDTO.UserId);
            if (usuario == null) { return NotFound(); }
            await userManager.RemoveClaimAsync(usuario, new Claim(ClaimTypes.Role, editarRolDTO.RoleName));
            await userManager.RemoveFromRoleAsync(usuario, editarRolDTO.RoleName);
            return Ok();
        }
    }

    public class EditarRolDTO
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}