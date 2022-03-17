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
    [Route("server/Klinika")]
    public class KlinikaController : ControllerBase
    {


        public ClinicWebContext Context { get; set; }

        public KlinikaController(ClinicWebContext context)
        {
            Context = context;
        }

        [Route("GetKlinike")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Klinika>>> VratiKlinike()
        {
            try
            {
                var klinike = await Context.Klinike.Include(p => p.grad).ToListAsync();
                if (klinike.Any())
                    return Ok(klinike);

                return NotFound("Ne postoje klinike!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("GetKlinike/{idGrada}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Klinika>>> VratiKlinike(int idGrada)
        {
            try
            {
                var klinike = await Context.Klinike.Where(p => p.grad.ID == idGrada).ToListAsync();
                if(klinike.Any())
                    return Ok(klinike);
                return NotFound("Ne postoje klinike u ovom gradu!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("GetKlinika/{id}")]
        [HttpGet]
        public async Task<ActionResult<Klinika>> GetKlinika(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }
            try
            {
                var klinika = await Context.Klinike.FindAsync(id);
                if (klinika == null)
                    return NotFound("Klinika nije pronadjena!");

                return Ok(klinika);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("PutKlinika")]
        [HttpPut]
        public async Task<ActionResult> PutKlinika([FromBody] Klinika klinika)
        {
            if (klinika.ID <= 0)
            {
                return BadRequest("Pogresan ID!");
            }
            try
            {
                Context.Klinike.Update(klinika);
                Context.Entry(klinika).State = EntityState.Modified;
                await Context.SaveChangesAsync();     
            }
            catch (Exception e)
            {
                if (!PostojiKlinika(klinika.ID))
                {
                    return NotFound("Nije prondajena klinika sa ovim id-jem");
                }
                else
                {
                    BadRequest(e.Message);
                }
            }

            return Ok("Uspesno azurirana klinika!");
        }

        [Route("PostKlinika/{imeGrada}/{nazivKlinike}/{adresa}")]
        [HttpPost]
        public async Task<ActionResult> PostKlinika(string imeGrada, string nazivKlinike, string adresa)
        {
            Klinika temp = new Klinika();

            temp.grad = Context.Gradovi.Where(p => p.imeGrada.ToLower() == imeGrada.ToLower()).FirstOrDefault();
            temp.nazivKlinike = nazivKlinike;
            temp.Adresa = adresa;

            if (temp.grad == null)
            {
                return BadRequest("Pogresan grad!");
            }
            if (string.IsNullOrWhiteSpace(temp.nazivKlinike))
            {
                return BadRequest("Pogresan naziv klinike!");
            }
            if (string.IsNullOrWhiteSpace(temp.Adresa))
            {
                return BadRequest("Pogresna adresa klinike!");
            }

            var klinika = Context.Klinike
                            .Where(p => p.grad == temp.grad
                            && p.nazivKlinike == temp.nazivKlinike
                            && p.Adresa == temp.Adresa).FirstOrDefault();  // Da li ovakva klinika vec postoji
            if (klinika == null)
            {
                try
                {
                    Context.Klinike.Add(temp);
                    await Context.SaveChangesAsync();
                    return Ok("Klinika je dodata!");

                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }

            return BadRequest("Klinika vec postoji!");
        }


        [Route("DeleteKlinika/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteKlinika(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pogresan ID!");
            }
            try
            {
                var klinika = await Context.Klinike.FindAsync(id);
                if (klinika == null)
                {
                    return NotFound("Klinika nije pronadjena!");
                }

                Context.Klinike.Remove(klinika);
                await Context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                BadRequest(e.Message);
            }
            return Ok("Klinika uspesno obrisana!");
        }

        private bool PostojiKlinika(int id)
        {
            return Context.Klinike.Any(p => p.ID == id);
        }



    }
}
