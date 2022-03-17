using System.ComponentModel.DataAnnotations;

namespace Models{

    public class Grad{ 

        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        [RegularExpression("^[a-zA-Z ]*$")]
        public string imeGrada { get; set; }

    }
}