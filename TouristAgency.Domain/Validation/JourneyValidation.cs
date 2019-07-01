using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Domain.Models.EfModels;

namespace TouristAgency.Domain.Validation
{
    public class JourneyValidation
    {
        public Journey Journey { get; set; }
        private ValidationError ValidationError { get; set; }

        public JourneyValidation(Journey Journey)
        {
            this.Journey = Journey;
            ValidationError = new ValidationError();
        }

        protected virtual bool IsStartDateValid()
        {
            if (Journey.StartedDate > Journey.ExpirstionDate)
            {
                ValidationError.ErrorValidation += " Стартовая дата не может быть больше конечной даты! ";
                return false;
            }
            return true;
        }

        protected virtual bool IsStartingDateNotLessThanToday()
        {
            if (Journey.StartedDate < DateTime.Now)
            {
                ValidationError.ErrorValidation += " Стартовая дата не может быть больше сегодняшней даты! ";
                return false;
            }
            return true;
        }

        protected virtual bool IsJourneyLengthNotMoreTwoMonth()
        {
            if (Journey.StartedDate != null && Journey.ExpirstionDate != null)
            {
                if (Journey.DateDifference() > 2)
                {
                    ValidationError.ErrorValidation += " Продолжительность поездки не может быть более двух месяцев! ";
                    return false;
                }
                return true;
            }
            else return false;
        }

        protected virtual bool IsJourneyAllocatedNotMoreTwoThousand()
        {
            if (Journey.StartedAmount > 2000)
            {
                ValidationError.ErrorValidation += " Количество выделенных путевок не может превышать 2000! ";
                return false;
            }
            return true;
        }

        protected virtual bool IsJourneyAllocatedNotMoreTwoThousands()
        {
            if (Journey.QuantitySold > Journey.StartedAmount)
            {
                ValidationError.ErrorValidation += " Количество выделенных путевок не может превышать количество проданных путевок! ";
                return false;
            }
            return true;
        }

        protected virtual bool IsJourneyQuantitySoldLessThenZero()
        {
            if (Journey.QuantitySold < 0)
            {
                ValidationError.ErrorValidation += " Количество проданных путевок не может быть меньше нуля! ";
                return false;
            }
            return true;
        }

        protected virtual bool IsJourneyStartedAmountLessThenOne()
        {
            if (Journey.StartedAmount < 1)
            {
                ValidationError.ErrorValidation += " Количество выделенных путевок не может быть меньше одной! ";
                return false;
            }
            return true;
        }

        public virtual ValidationError IsValidationSuccessful()
        {
            //IsJourneyAllocatedNotMoreTwoThousand();
            //IsJourneyLengthNotMoreTwoMonth();
            //IsStartingDateNotLessThanToday();
            //IsStartDateValid();
            //IsJourneyAllocatedNotMoreTwoThousands();
            //IsJourneyQuantitySoldLessThenZero();
            //IsJourneyStartedAmountLessThenOne();
            ValidationError.Validation = IsJourneyAllocatedNotMoreTwoThousand() && IsJourneyLengthNotMoreTwoMonth() && IsStartingDateNotLessThanToday() && IsStartDateValid() && IsJourneyAllocatedNotMoreTwoThousands() && IsJourneyQuantitySoldLessThenZero() && IsJourneyStartedAmountLessThenOne();
            return ValidationError;
        }
    }
}
