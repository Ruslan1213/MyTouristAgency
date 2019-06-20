using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.WebUI.Models;

namespace TouristAgency.Domain.Models.UserCreatrdByAdmin
{
    public class UserCreatedByAdmin: RegisterViewModel
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
