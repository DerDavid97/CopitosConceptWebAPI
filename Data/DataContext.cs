using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base() // Usually would be a DBContext, for testing this will simply hold enumerables 
        {
            Kontakte =
            [
                new Kontakt() { Id = 1, Anrede = "Herr", Vorname = "Max", Nachname = "Mustermann", Geburtsdatum = new DateTime(1990,05,04).ToShortDateString(), PLZ = "12345", Ort="Ort", Land="DE"  },
            ];
            
        }

        public List<Kontakt> Kontakte { get; set; }
    }
}
