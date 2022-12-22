using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
namespace Shop.Controllers
{
    [Route("products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> Get(
           [FromServices] DataContext context)
        {
            var product = await context.Products
            .Include(item => item.Category)
            .AsNoTracking()
            .ToListAsync();
            return Ok(product);
        }
        //-------------------
        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetById(
            int id, [FromServices] DataContext context)
        {
            var product = await context.Products
            .Include(item => item.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(
                item => item.Id == id);
            return Ok(product);

        }
        //--------------------- 
        [HttpGet]
        [Route("categories/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> GetByCategory(
            int id, [FromServices] DataContext context)
        {
            var product = await context.Products
            .Include(item => item.Category)
            .AsNoTracking()
            .Where(
                item => item.CategoryId == id)
            .ToListAsync();

            return Ok(product);

        }
        //---------------------
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "employee")]

        public async Task<ActionResult<List<Product>>> Post(
            [FromBody] Product model,
            [FromServices] DataContext context)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Nao foi possivel criar produto",
                    erro = ex
                });
            }

        }
        //---------------------
        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]

        //-----------------------
        public async Task<ActionResult<List<Product>>> Put(
          int id,
         [FromBody] Product model,
         [FromServices] DataContext context)
        {
            if (model.Id != id)
                return BadRequest(new
                { message = "produto nao encontrada!" });

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Product>(model).State =
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
                return BadRequest(new { message = "Nao foi possivel atualizar a produto" });
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]

        //----------------------
        public async Task<ActionResult<List<Product>>> Delete(
            int id,
            [FromServices] DataContext context)
        {
            var product = await context.Products
                .FirstOrDefaultAsync(
                    item => item.Id == id
                );
            if (product == null) return NotFound(
                new { message = "produto nao encontrada!" });

            try
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
                return Ok(product);
            }
            catch (Exception)
            {
                return BadRequest(
                new { message = "Nao foi possivel remover!" });
            }
        }
    }
}