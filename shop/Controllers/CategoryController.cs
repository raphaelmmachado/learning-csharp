using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

// Endpoint 
// https://localhost:5001/exemplo
// [Route("exemplo")]
namespace Shop.Controllers
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Category>>> Get(
            [FromServices] DataContext context)
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();
            return Ok(categories);
        }
        //-------------------
        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> GetById(
            int id, [FromServices] DataContext context)
        {
            var category = await context.Categories
                .AsNoTracking().FirstOrDefaultAsync(
                    item => item.Id == id);
            return Ok(category);

        }
        //---------------------
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<List<Category>>> Post(
            [FromBody] Category model,
            [FromServices] DataContext context)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Nao foi possivel criar categoria",
                    erro = ex
                });
            }

        }
        //---------------------
        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "employee")]

        //-----------------------
        public async Task<ActionResult<List<Category>>> Put(
          int id,
         [FromBody] Category model,
         [FromServices] DataContext context)
        {
            if (model.Id != id)
                return BadRequest(new
                { message = "Categoria nao encontrada!" });

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Category>(model).State =
                EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Este registro ja foi atualizado" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Nao foi possivel atualizar a categoria" });
            }
        }
        //----------------------
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<List<Category>>> Delete(
            int id,
            [FromServices] DataContext context)
        {
            var category = await context.Categories
                .FirstOrDefaultAsync(
                    item => item.Id == id
                );
            if (category == null) return NotFound(
                new { message = "Categoria nao encontrada!" });

            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok(category);
            }
            catch (Exception)
            {
                return BadRequest(
                new { message = "Nao foi possivel remover!" });
            }
        }
    }
}