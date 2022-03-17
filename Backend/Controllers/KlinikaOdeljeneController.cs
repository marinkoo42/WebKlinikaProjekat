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
    [Route("server/KlinikaOdeljene")]
    public class KlinikaOdeljenjeController : ControllerBase
    {
     
        public ClinicWebContext Context { get; set; }

        public KlinikaOdeljenjeController(ClinicWebContext context){
            Context = context;
        }

        
        [Route("GetKlinikeOdeljenja")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KlinikaOdeljenje>>> GetKlinikeOdeljenja()
        {
            try
            {
                return await Context.KlinikeOdeljenja
                                .Include(p => p.klinika)
                                .Include(p => p.odeljenje)
                                .ToListAsync();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
        [Route("GetKlinikaOdeljenje/{klinikaId}")]
        [HttpGet]
        public async Task<ActionResult<KlinikaOdeljenje>> GetKlinikaOdeljenje( int klinikaId)
        {
            if (klinikaId <= 0) 
            {
                return BadRequest("Pogrešan ID!");
            }
            try
            {
                var klinikaOdeljenje = await Context.KlinikeOdeljenja
                                        .Include(p => p.odeljenje)
                                        .Where(p => p.klinika.ID == klinikaId).ToListAsync();

                if (!klinikaOdeljenje.Any())
                {
                    return NotFound("Nije pronadjeno!");
                }

                return Ok(klinikaOdeljenje);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("GetKlinikaOdeljenjeBy/{id}")]
        [HttpGet]
        public async Task<ActionResult<KlinikaOdeljenje>> getKlinikaOdeljenje(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }
            try
            {
                var klinikaOdeljenje = await Context.KlinikeOdeljenja
                                        .Include(p => p.odeljenje)
                                        .Where(p => p.ID == id).ToListAsync();

                if (!klinikaOdeljenje.Any())
                {
                    return NotFound("Nije pronadjeno!");
                }

                return Ok(klinikaOdeljenje);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("GetSlobodniTermini/{id}")]
        [HttpGet]
        public ActionResult<KlinikaOdeljenje> getSlobodniTermini(int id)                            //broj slobodnih termina
        {
            if (id <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }
            try
            {
                KlinikaOdeljenje temp = Context.KlinikeOdeljenja.Find(id);
                if(temp==null)
                    return BadRequest("Ne postoji!");

                int br = Context.Rezervacije.Where(p => p.KlinikaOdeljenje == temp).Count();

                return Ok(10 - br);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("PutKlinikaOdeljenje")]
        [HttpPut]
        public async Task<ActionResult> PutKlinikaOdeljenje([FromBody] KlinikaOdeljenje klinikaOdeljenje)
        {
            if (klinikaOdeljenje.ID <= 0)
            {
                return BadRequest("Pogresan ID!");
            }

            Context.Entry(klinikaOdeljenje).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (!PostojiKlinikaOdeljenje(klinikaOdeljenje.ID))
                {
                    return NotFound("Nije pronadjeno!");
                }
                else
                {
                    return BadRequest(e.Message);
                }
            }

            return Ok("Uspesno izmenjeno!");
        }

        [Route("PostKlinikaOdeljenje/{odeljenjeId}/{klinikaId}/{lekar}")]
        [HttpPost]
        public async Task<ActionResult<KlinikaOdeljenje>> PostKlinikaOdeljenje(int odeljenjeId,int klinikaId, string lekar)
        {
            if (odeljenjeId <= 0 || klinikaId<=0)
            {
                return BadRequest("Pogresan ID!");
            }
            if(string.IsNullOrWhiteSpace(lekar))
            {
                return BadRequest("Unesite ime i prezime lekara");
            }
            try
            {
                Odeljenje odeljenje = Context.Odeljenja.Find(odeljenjeId);
                Klinika klinika = Context.Klinike.Find(klinikaId);

                KlinikaOdeljenje temp = new KlinikaOdeljenje();
                temp.klinika = klinika;
                temp.odeljenje = odeljenje;
                temp.lekar = lekar;

                Context.KlinikeOdeljenja.Add(temp);
                await Context.SaveChangesAsync();

                return Ok("Uspesno dodato!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private bool PostojiKlinikaOdeljenje(int id)
        {
            return Context.KlinikeOdeljenja.Any(p => p.ID == id);
        }


        [Route("DeleteKlinikaOdeljenje/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteKlinikaOdeljenje(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pogresan ID!");
            }
            try
            {
                var klinikaOdeljenje = await Context.KlinikeOdeljenja.FindAsync(id);
                if (klinikaOdeljenje == null)
                {
                    return NotFound("Nije pronadjeno!");
                }

                Context.KlinikeOdeljenja.Remove(klinikaOdeljenje);
                await Context.SaveChangesAsync();
                return Ok("Uspesno izbrisano!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }


    }
}
