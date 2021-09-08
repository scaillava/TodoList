using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Todo.Api.ViewModels
{
    [DataContract]
    public class RegisterViewModel
    {
        [Required]
        [DataMember]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [DataMember]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DataMember]
        public string Password { get; set; }

       
    }
}
