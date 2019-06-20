using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Domain.Models.EfModels;

namespace TouristAgency.Domain.Validation
{
    public class JourneyValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Journey journey = value as Journey;
            if (journey.StartedDate < journey.ExpirstionDate && journey.StartedDate > DateTime.Now && journey.DateDifference() < 2 && journey.StartedAmount < 2000)
            {
                return false;
            }
            return true && base.IsValid(value);
        }
    }
}
