using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Todo.Api.ViewModels
{
    [DataContract]
    public class UpsertTodoViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [Required]
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public int Position { get; set; }
        [DataMember]
        public List<UpsertTodoTaskViewModel> TodoChecks { get; set; }
    }


}
