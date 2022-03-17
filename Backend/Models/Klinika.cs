
using System.ComponentModel.DataAnnotations;

namespace Models{

    public class Klinika{

        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string nazivKlinike { get; set; }

        [Required]
        public Grad grad { get; set; }

        [Required]
        [MaxLength(50)]
        public string Adresa { get; set; }

    }

}