using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KontaktController : Controller
    {

        private readonly DataContext _context;

        public KontaktController(DataContext dataContext)
        {
            _context = dataContext;
        }

        [HttpGet]
        public ActionResult<List<Kontakt>> GetAllKontakte()
        {
            var kontakt = _context.Kontakte.ToList();

            return Ok(kontakt);
        }

        [HttpGet("{id}")]
        public ActionResult<Kontakt> GetKontakt(int id)
        {

            var kontakt = _context.Kontakte.FirstOrDefault(x => x.Id == id);
            if (kontakt == null)
            {
                return NotFound($"Kontakt ID: {id} not found");
            }

            return Ok(kontakt);
        }

        [HttpPost]
        public ActionResult<Kontakt> AddKontakt(Kontakt kontakt)
        {

            if (_context.Kontakte.FirstOrDefault(x => x.Id == kontakt.Id) != null)
                return NotFound($"Kontakt ID: {kontakt.Id} already exists");
            if (!TryUpdateGeburtsdatum(kontakt.Geburtsdatum, out DateTime geburtsdatum))
                return BadRequest("Invalid or future Geburtsdatum.");
            if (kontakt.PLZ.Length != 5 || !int.TryParse(kontakt.PLZ, out _))
                return BadRequest("PLZ must be 5 digits");


            _context.Kontakte.Add(kontakt);
            return Ok(_context.Kontakte.ToList());
        }

        [HttpPut]
        public ActionResult<Kontakt> UpdateKontakt(Kontakt updatedKontakt)
        {
            if (_context.Kontakte.FirstOrDefault(x => x.Id == updatedKontakt.Id) == null)
                return NotFound($"Kontakt ID: {updatedKontakt.Id} not found");
            if (!TryUpdateGeburtsdatum(updatedKontakt.Geburtsdatum, out DateTime geburtsdatum))
                return BadRequest("Invalid or future Geburtsdatum.");
            if (updatedKontakt.PLZ.Length != 5 || !int.TryParse(updatedKontakt.PLZ, out _))
                return BadRequest("PLZ must be 5 digits");
            _context.Kontakte.First(x => x.Id == updatedKontakt.Id).Anrede = updatedKontakt.Anrede;
            _context.Kontakte.First(x => x.Id == updatedKontakt.Id).Vorname = updatedKontakt.Vorname;
            _context.Kontakte.First(x => x.Id == updatedKontakt.Id).Nachname = updatedKontakt.Nachname;
            _context.Kontakte.First(x => x.Id == updatedKontakt.Id).Geburtsdatum = geburtsdatum.ToShortDateString();
            _context.Kontakte.First(x => x.Id == updatedKontakt.Id).PLZ = updatedKontakt.PLZ;
            _context.Kontakte.First(x => x.Id == updatedKontakt.Id).Ort = updatedKontakt.Ort;
            _context.Kontakte.First(x => x.Id == updatedKontakt.Id).Land = updatedKontakt.Land;


            return CreatedAtAction(nameof(GetKontakt), new { id = updatedKontakt.Id }, updatedKontakt);
        }

        [HttpDelete("{id}")]
        public ActionResult<List<Kontakt>> UpdateKontakt(int id)
        {
            if (_context.Kontakte.FirstOrDefault(x => x.Id == id) != null)
                return NotFound($"Kontakt ID: {id} not found");
            else
                _context.Kontakte.Remove(item: _context.Kontakte.First(x => x.Id == id));

            return Ok(_context.Kontakte.ToList());
        }
        private bool TryUpdateGeburtsdatum(string geburtsdatumString, out DateTime geburtsdatum) // Method in the controller class have to be either private or need a HttpAtribute
        {
            if (DateTime.TryParse(geburtsdatumString, out geburtsdatum))
            {
                if (geburtsdatum < DateTime.Now)
                {
                    return true; // date is valid 
                }
            }

            return false;
        }
    }
}
