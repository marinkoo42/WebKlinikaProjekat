using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Models{

    public class KlinikaOdeljenje{
        
        [Key]
        public int ID { get; set; }

        [Required]
        public virtual Klinika klinika { get; set; }

        [Required]
        public virtual Odeljenje odeljenje { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z ]*$")]
        public String lekar { get; set; }

    }


}