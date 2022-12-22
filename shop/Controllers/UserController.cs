using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<User>>> Get(
            [FromServices] DataContext context)
        {
            var users = await context
            .Users
            .AsNoTracking()
            .ToListAsync();

            return Ok(users);
        }



        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        // CORRETO  ->  [Authorize(Roles = "manager")]
        // por questoes de desenvolvimento deixamos como anonimo
        public async Task<ActionResult<User>> Post(
            [FromServices] DataContext context,
            [FromBody] User model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                context.Users.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Nao foi possivel criar o Usuario"
                });
            }
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate(
        [FromServices] DataContext context,
        [FromBody] User model)
        {
            var user = await context.Users
            .AsNoTracking()
            .Where(x => x.Username == model.Username &&
                        x.Password == model.Password
            ).FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new
                {
                    message = "Usuario ou senha invalidos."
                });
            var token = TokenService.GenerateToken(user);
            return new
            {
                user = user,
                token = token
            };
        }


        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Put(
            [FromServices] DataContext context,
            int id,
            [FromBody] User model)
        {
            //verifica se os dados sao validos
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            //verifica se o ID e o mesmo do modelo
            if (id != model.Id)
                return NotFound(new { message = "Usuario nao encontrado" });
            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Nao foi possivel atualizar usuario",
                    erro = ex
                });
            }
        }
    }
}