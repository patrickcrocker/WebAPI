using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Treasure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Location { get; set; }

        [DataType(DataType.Url)]
        public string PhotoLink { get; set; }

        [DataType(DataType.Currency)]
        public decimal Value { get; set; }

        [Display(Name = "Date Acquired")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AcquiredDate { get; set; }

        public string PurchaseFrom { get; set; }

        [DataType(DataType.Currency)]
        public decimal PurchasePrice { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }
    }
}
