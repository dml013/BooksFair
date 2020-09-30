using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BooksFair.Models {
    public class Product {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Range(1, 10000)]
        public double ListPrice { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price50 { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price100 { get; set; }

        [Required]
        [DisplayName("Image")]
        [DefaultValue("empty")]
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        //[ForeignKey(nameof(CoverTypeId))]
        public CoverType CoverType { get; set; }
        
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int CoverTypeId { get; set; }

    }
}
