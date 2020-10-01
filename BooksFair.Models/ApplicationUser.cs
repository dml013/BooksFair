using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace BooksFair.Models {
    public class ApplicationUser : IdentityUser {
        [Required]
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        [NotMapped] //don't adding to db
        public string Role { get; set; }
    }
}
