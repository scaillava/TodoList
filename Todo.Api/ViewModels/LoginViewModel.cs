using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Todo.Api.ViewModels
{
    [DataContract]
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [DataMember]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DataMember]
        public string Password { get; set; }

    }

    public class TokenViewModel
    {
        public Guid AuthToken { get; set; }
        public DateTime Expiration { get; set; }

    }
}
