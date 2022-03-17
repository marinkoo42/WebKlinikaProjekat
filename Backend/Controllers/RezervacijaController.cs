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
    [Route("rezervacija")]
    public class RezervacijaController : ControllerBase
    {

        public ClinicWebContext Context { get; set; }

        public RezervacijaController(ClinicWebContext context)
        {
            Context = context;
        }

        [Route("ZauzetiTermini")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rezervacija>>> ZauzetiTermini(int KlinikaOdeljeneId, DateTime datum)
        {
            if (KlinikaOdeljeneId <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }
            if (datum.Date < DateTime.Today)
            {
                return BadRequest("Pogresan datum!");
            }
            try
            {
                var rezervacije = await Context.Rezervacije
                    .Where(p => p.KlinikaOdeljenje.ID == KlinikaOdeljeneId && p.datum.Date == datum.Date)

                    .ToListAsync();

                if(rezervacije.Any())
                    return Ok(rezervacije.Select(p =>
                   new
                   {
                       p.ID,
                       p.KlinikaOdeljenje,
                       p.termin,
                       p.datum.Date
            }));
                return NotFound("Nema rezervacija!");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("ProveriRezervaciju")]
        [HttpGet]
        public async Task<ActionResult> proveriRezervaciju(string email)
        {
            try
            {
                var rezervacije = Context.Rezervacije
                .Where(p => p.email == email)
                .Include(p => p.KlinikaOdeljenje)
                .Include(p => p.KlinikaOdeljenje.odeljenje)
                .Include(p => p.KlinikaOdeljenje.klinika)
                .ThenInclude(p => p.grad);
                if (await rezervacije.AnyAsync())
                {
                    return Ok(rezervacije);    
                }
                else
                {
                    return NotFound("Ne postoji rezervacija sa unetim podacima!");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PostRezervacija/{klinikaOdeljenjeId}/{termin}/{datum}/{email}")]
        [HttpPost]
        public async Task<ActionResult> PostRezervacija(int klinikaOdeljenjeId, int termin, DateTime datum, string email)
        {
            if (klinikaOdeljenjeId <= 0)
            {
                return BadRequest("Pogresan ID!");
            }
            if (termin < 1 || termin > 10)
            {
                return BadRequest("Pogresan termin!");
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Pogresan email!");
            }
            if(datum.Date < DateTime.Today)
            {
                return BadRequest("Pogresan datum!");
            }
            try
            {
                Rezervacija rezervacija = new Rezervacija();
                rezervacija.termin = termin;
                rezervacija.email = email;
                rezervacija.datum = datum;
                rezervacija.KlinikaOdeljenje = Context.KlinikeOdeljenja.Find(klinikaOdeljenjeId);

                if (Context.Rezervacije.Where(p => p.KlinikaOdeljenje.ID == klinikaOdeljenjeId && p.datum == datum && p.termin == termin).Any())
                    return BadRequest("Termin je vec rezervisan!");

                Context.Rezervacije.Add(rezervacija);
                await Context.SaveChangesAsync();
                return Ok(rezervacija);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DeleteRezervacija/{id}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteRezervacija(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pogresan ID!");
            }
            try
            {
                var rezervacije = Context.Rezervacije.Where(p => p.ID == id);
                if (rezervacije.Any())
                {
                    Context.Rezervacije.Remove(rezervacije.First());
                    await Context.SaveChangesAsync();
                    return Ok("Rezervacija je obrisana!");
                }
                else
                {
                    return NotFound("Ne postoji rezervacija!");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PutRezervacija/{id}/{email}")]
        [HttpPut]
        public async Task<ActionResult> PutRezervacija(int id, string email)
        {
            if (id < 0)
                return BadRequest("Pogresan ID!");

            if(string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Pogresan email!");
            }
            
            
            try
            {

                var rezervacija = Context.Rezervacije.Find(id);
                if (rezervacija == null)
                    return NotFound("Ne postoji podatak sa ovim ID-jem");

                rezervacija.email = email;
                await Context.SaveChangesAsync();
                return Ok("Uspesno azurirani podaci!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}