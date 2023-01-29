using Entities.Concrete.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class User:AuditableEntity
    {
        [MinLength(5)]
        public string UserName { get; set; }
        [MinLength(5)]
        public string FirstName { get; set; }
        [MinLength(5)]
        public string LastName { get; set; }
        [MinLength(5)]
        public string Password { get; set; }

        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        [MinLength(5)]
        public string Email { get; set; }
        [MinLength(5)]
        public string Address { get; set; }
    }
}
