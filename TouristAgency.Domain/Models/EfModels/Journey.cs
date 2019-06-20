using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TouristAgency.Domain.Models.EfModels
{
    public class Journey
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Journey()
        {
            this.Orders = new HashSet<Order>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdJourney { get; set; }
        [Required]
        [Display(Name = "Описание")]
        public Nullable<int> IdTour { get; set; }
        [Required]
        [Display(Name = "Дата начала")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> StartedDate { get; set; }
        [Required]
        [Display(Name = "Конечная дата")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ExpirstionDate { get; set; }
        [Required]
        [Display(Name = "Выделено путевок")]
        public Nullable<int> StartedAmount { get; set; }
        [Display(Name = "Продано путевок")]
        public Nullable<int> QuantitySold { get; set; }
        [Required]
        [Display(Name = "Горящая")]
        public bool IsLastMinuteTrip { get; set; }
        [HiddenInput(DisplayValue = false)]
        public bool IsDeleted { get; set; }

        public int DateDifference()
        {
            if (this.ExpirstionDate.Value.Year == this.StartedDate.Value.Year)
                return this.ExpirstionDate.Value.Month - this.StartedDate.Value.Month;
            else
                return (this.ExpirstionDate.Value.Year - this.StartedDate.Value.Year) * 12;
        }

        public virtual Tour Tour { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

    }
}
