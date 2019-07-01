using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Domain.Models.EfModels
{
    public class Tour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tour()
        {
            this.Journeys = new HashSet<Journey>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTour { get; set; }
        public Nullable<int> IdTypeTour { get; set; }
        public Nullable<int> IdHotelsType { get; set; }
        [Required]
        [Display(Name = "Название тура")]
        [MaxLength(35)]
        [StringLength(256, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 10 до 256 символов")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Цена")]
        public Nullable<int> Price { get; set; }
        [Required]
        [Display(Name = "Кол. Людей ОТ")]
        public Nullable<int> StartNumberOfPeople { get; set; }
        [Required]
        [Display(Name = "Кол. Людей ДО")]
        public Nullable<int> EndNumberOfPeople { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        [Display(Name = "Описание")]
        [MaxLength(256)]
        [StringLength(256, MinimumLength = 10, ErrorMessage = "Длина строки должна быть от 10 до 256 символов")]
        public string Discription { get; set; }

        public virtual HotelsType HotelsType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Journey> Journeys { get; set; }
        public virtual TypesTour TypesTour { get; set; }

    }
}
