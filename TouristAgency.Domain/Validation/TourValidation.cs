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

        public ValidationError Error { get; set; }
        public TourValidation(Tour tour)
        {
            this.tour = tour;
            Error = new ValidationError();
        }

        private bool IsEndNumberOfPeopleValid()
        {
            if (tour.EndNumberOfPeople < 0)
            {
                Error.ErrorValidation += " Конечное количество людей не может быть меньше нуля! ";
                return false;
            }
            else return true;
        }

        private bool IsStartNumberOfPeopleValid()
        {
            if (tour.StartNumberOfPeople < 0)
            {
                Error.ErrorValidation += " Стартовое количество людей не может быть меньше нуля! ";
                return false;
            }
            else return true;
        }

        private bool IsPriseValid()
        {
            if (tour.Price <= 0)
            {
                Error.ErrorValidation += " Цена тура не может быть меньше 1! ";
                return false;
            }
            else return true;
        }

        private bool IsStartNumberOfPeopleLessEndNumberOfPeople()
        {
            if (tour.StartNumberOfPeople > tour.EndNumberOfPeople)
            {
                Error.ErrorValidation += " Стартовое количество человек не может быть больше конечного количества человек! ";
                return false;
            }
            else return true;
        }

        private bool IsStartNumberOfPeopleLessThenTenThousand()
        {
            if (tour.StartNumberOfPeople > 10000)
            {
                Error.ErrorValidation += " Стартовое количество человек не может быть больше 10000! ";
                return false;
            }
            else return true;
        }

        public virtual ValidationError IsValidationSuccessful()
        {
            Error.Validation= IsEndNumberOfPeopleValid() && IsStartNumberOfPeopleValid() && IsPriseValid()&& IsStartNumberOfPeopleLessEndNumberOfPeople() && IsStartNumberOfPeopleLessThenTenThousand();
            return Error;
        }
    }
}
