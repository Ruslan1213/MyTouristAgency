using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.WebUI.Models;

namespace TouristAgency.Domain.Models.EfModels
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrder { get; set; }
        public Nullable<int> IdJourney { get; set; }
        [Display(Name = "Скидка")]
        public Nullable<double> Discount { get; set; }
        [Display(Name = "Колличество запрашиваемых путевок")]
        [Range(1, 20, ErrorMessage = "Недопустимое значение")]
        public Nullable<int> CountOfJourneys { get; set; }
        public Nullable<int> OrderStatus_IdOrder { get; set; }

        public virtual Journey Journey { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual ApplicationMyUser User { get; set; }
    }
}
