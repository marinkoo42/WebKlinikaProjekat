using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ClinicWeb.Controllers
{
    [ApiController]
    [Route("server/Gradovi")]
    public class GradController : ControllerBase
    {
     

        public ClinicWebContext Context { get; set; }

        public GradController(ClinicWebContext context){
            Context = context;
        }

        [Route("GetGradovi")]
        [HttpGet]
        public async Task<ActionResult> GetGradovi(){

            try
            {
                var gradovi = await Context.Gradovi.ToListAsync();
                return Ok(gradovi);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PostGrad")]
        [HttpPost]
        public async Task<ActionResult> PostGrad([FromBody] Grad grad)
        {
            if(string.IsNullOrWhiteSpace(grad.imeGrada))
                return BadRequest("Neispravno ime grada!");
            try
            {
                Context.Gradovi.Add(grad);
                await Context.SaveChangesAsync();
                return Ok(grad);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PutGrad")]
        [HttpPut]
        public async Task<ActionResult> PutGrad([FromBody] Grad grad)
        {
            if (grad.ID <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }

            Context.Entry(grad).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (!PostojiGrad(grad.ID))
                {
                    return NotFound("Grad nije pronadjen!");
                }
                else
                {
                    return BadRequest(e.Message);
                }
            }

            return Ok("Grad azuriran!");
        }

        [Route("DeleteGrad/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteGrad(int id)
        {
            try
            {
                var grad = await Context.Gradovi.FindAsync(id);
                if (grad == null)
                {
                    return NotFound("Grad nije pronadjen!");
                }

                Context.Gradovi.Remove(grad);
                await Context.SaveChangesAsync();

                return Ok("Grad je uspesno izbrisan!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private bool PostojiGrad(int id)
        {   
            return Context.Gradovi.Any(p => p.ID == id);
        }

    }
}
