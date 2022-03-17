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
    [Route("[controller]")]
    public class ClinicWebController : ControllerBase
    {
     

        public ClinicWebContext Context { get; set; }

        public ClinicWebController(ClinicWebContext context){
            Context = context;
        }

        [HttpGet]
        [Route("gradovi")]
        public async Task<ActionResult> vratiGradove(){
            try
            {
                var gradovi = await Context.Gradovi.ToListAsync();
                if(!gradovi.Any())
                    return NotFound("Nema gradova!");
                return Ok(gradovi);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("Odeljenja/{idKlinike}")]
        public async Task<ActionResult> vratiOdeljenja(int idKlinike)
        {
            if (idKlinike <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }
            if(await Context.Klinike.FindAsync(idKlinike) == null)
            {
                return BadRequest("Klinika ne postoji!");
            }
            try
            {
                var odeljenja = await Context.KlinikeOdeljenja
                    .Where(p => p.klinika.ID == idKlinike)
                    .Include(p => p.odeljenje)
                    .ToListAsync();
                if (!odeljenja.Any())
                    return NotFound("Nema odeljenja u ovoj klinici!");
                return Ok(odeljenja.Select(p => new
                {
                    p.odeljenje,
                    p.lekar
                }).ToList());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
