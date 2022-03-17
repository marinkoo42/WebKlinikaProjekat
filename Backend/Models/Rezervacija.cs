using System;
using System.ComponentModel.DataAnnotations;

namespace Models{
    public class Rezervacija{
        
        [Key]
        public int ID { get; set; }

        [Required]
        public virtual KlinikaOdeljenje KlinikaOdeljenje { get; set; }

        [Required]
        [Range(1,10)]
        public int termin { get; set; }

        [Required]
        public DateTime datum { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

    }
}