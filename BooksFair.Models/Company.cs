using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BooksFair.Models {
    public class Company {
        public int Id { get; set; }
        [Required]
        [DisplayName("Company name")]
        public string Name { get; set; }

        [DisplayName("Street address")]
        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [DefaultValue(false)]
        [DisplayName("Is authorized company?")]
        public bool IsAuthorizedCompany { get; set; }

    }
}
