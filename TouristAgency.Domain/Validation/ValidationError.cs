using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Domain.Validation
{
    public class ValidationError
    {
        public bool Validation { get; set; }
        public string ErrorValidation { get; set; }

        public ValidationError()
        {
            Validation = true;
            ErrorValidation = "";
        }
    }
}
