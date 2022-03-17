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
    [Route("server/Odeljenje")]
    public class OdeljenjeController : ControllerBase
    {
     
        public ClinicWebContext Context { get; set; }

        public OdeljenjeController(ClinicWebContext context){
            Context = context;
        }

        
        [Route("GetOdeljenja")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Odeljenje>>> GetOdeljenja()
        {
            try
            {
                var odeljenja = await Context.Odeljenja.ToListAsync();
                if(odeljenja.Any())
                    return Ok(odeljenja);
                return NotFound("Ne postoje odeljenja!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
        [Route("GetOdeljenje/{id}")]
        [HttpGet]
        public async Task<ActionResult<Odeljenje>> GetOdeljenje(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }
            try
            {
                var odeljenje = await Context.Odeljenja.FindAsync(id);

                if (odeljenje == null)
                {
                    return NotFound("Odeljenje nije pronadjeno!");
                }

                return Ok(odeljenje);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PostOdeljenje")]
        [HttpPost]
        public async Task<ActionResult> PostOdeljenje([FromBody] Odeljenje odeljenje)
        {
            try
            {
                Context.Odeljenja.Add(odeljenje);
                await Context.SaveChangesAsync();

                return Ok("Odeljenje je dodato!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PostOdeljenje/{naziv}/{opis}/{slika}")]
        [HttpPost]
        public async Task<ActionResult> PostOdeljenje (string naziv, string opis, string slika)
        {
            if(string.IsNullOrWhiteSpace(naziv))
            {
                return BadRequest("Pogresan naziv odeljenja!");
            }
            if(string.IsNullOrWhiteSpace(opis))
            {
                return BadRequest("Pogresan opis odeljena!");
            }
            try
            {
                Odeljenje temp = new Odeljenje();
                temp.nazivOdeljenja = naziv;
                temp.opisOdeljenja = opis;
                temp.slikaOdeljenja = slika;

                Context.Odeljenja.Add(temp);
                await Context.SaveChangesAsync();

                return Ok("Odeljenje je dodato!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DeleteOdeljenje/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOdeljenje(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }
            try
            {
                var odeljenje = await Context.Odeljenja.FindAsync(id);
                if (odeljenje == null)
                {
                    return NotFound("Odeljenje nije pronadjeno!");
                }

                Context.Odeljenja.Remove(odeljenje);
                await Context.SaveChangesAsync();

                return Ok("Odeljenjen uspesno obrisano!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PutOdeljenje")]
        [HttpPut]
        public async Task<IActionResult> PutOdeljenje([FromBody] Odeljenje odeljenje)
        {
            if (odeljenje.ID <= 0)
            {
                return BadRequest();
            }

            Context.Entry(odeljenje).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (!PostojiOdeljenje(odeljenje.ID))
                {
                    return NotFound("Nije pronadjeno!");
                }
                else
                {
                    return BadRequest(e.Message);
                }
            }

            return Ok("Odeljenje uspesno izmenjeno!");
        }

        private bool PostojiOdeljenje(int id)
        {
            return Context.Odeljenja.Any(p => p.ID == id);
        }

    }
}
