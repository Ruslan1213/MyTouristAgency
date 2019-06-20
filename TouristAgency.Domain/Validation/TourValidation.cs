using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Domain.Models.EfModels;

namespace TouristAgency.Domain.Validation
{
    public class TourValidation
    {
        Tour tour;

        public TourValidation(Tour tour)
        {
            this.tour = tour;
        }

        private bool IsEndNumberOfPeopleValid() => tour.EndNumberOfPeople < 0 ? false : true;

        private bool IsStartNumberOfPeopleValid() => tour.StartNumberOfPeople < 0 ? false : true;

        private bool IsPriseValid() => tour.Price < 0 ? false : true;

        public virtual bool IsValidationSuccessful() => IsEndNumberOfPeopleValid() &&
            IsStartNumberOfPeopleValid() && IsPriseValid();
    }
}
