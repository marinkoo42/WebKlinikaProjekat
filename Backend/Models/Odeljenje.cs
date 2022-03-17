using System.ComponentModel.DataAnnotations;

namespace Models{

    public class Odeljenje{ 

        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string nazivOdeljenja { get; set; }

        [Required]
        [MaxLength(100)]
        public string opisOdeljenja { get; set; }

        [Required]
        public string slikaOdeljenja {get;set;}

    }
}