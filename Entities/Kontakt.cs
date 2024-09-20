using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities
{
    public class Kontakt
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Anrede ist erforderlich.")]
        public required string Anrede{ get; set; }
        [Required(ErrorMessage = "Vorname ist erforderlich.")]
        public required string Vorname { get; set; }
        [Required(ErrorMessage = "Nachname ist erforderlich.")]
        public required string Nachname { get; set; }
        [Required(ErrorMessage = "Geburtsdatum ist erforderlich.")]
        public required string Geburtsdatum { get; set; }
        [Required(ErrorMessage = "PLZ ist erforderlich.")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "PLZ muss genau 5 Zeichen lang sein.")]
        public required string PLZ { get; set; }
        public string? Ort { get; set; }
        [StringLength(2, ErrorMessage = "Land muss genau 2 Zeichen lang sein.")] 
        public string? Land { get; set; }

    }
}
