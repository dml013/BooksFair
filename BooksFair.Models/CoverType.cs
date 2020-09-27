using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BooksFair.Models {
    public class CoverType {
        public int Id { get; set; }

        [Required]
        [DisplayName("Cover type")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
